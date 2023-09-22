//const { "ep-frosty-firefly-23612795.eu-central-1.aws.neon.tech","Flashcards", 'charls7c', 'r3Y97kyZ.KE5NqA','ep-frosty-firefly-23612795'} = process.env;

// app.js
const express = require('express');
const { Pool } = require('pg')
const cors = require('cors');


//const URL = `postgres://charls7c:r3Y97kyZ.KE5NqA@ep-frosty-firefly-23612795.eu-central-1.aws.neon.tech/Flashcards?options=project%3Dep-frosty-firefly-23612795`;
const app = express();


app.use(function(req, res, next) {
    res.header("Access-Control-Allow-Origin", "*");
    res.header("Access-Control-Allow-Headers", "X-Requested-With");
    next();
});
app.use(cors());
const pool = new Pool({
    host: 'ep-frosty-firefly-23612795.eu-central-1.aws.neon.tech',
    user: 'charls7c',
    password: 'vhp9Onw0XHLr',
    database: 'Flashcards',
    ssl: {
        rejectUnauthorized: false,
        sslmode: 'require'
    }
});





app.get('/words', async (req, res) => {
    
    try {
        const result = await pool.query('SELECT word,hint FROM Words_english ORDER BY RANDOM() LIMIT 1');
        
        res.json(result.rows);
    } catch (err) {
        console.error(err);
        res.status(500).send('An error occurred');
    }
});
app.get('/test-connection', async (req, res) => {
    try {
        await pool.query('SELECT NOW()');
        res.send('Connection to database successful');
    } catch (err) {
        console.error(err);
        res.status(500).send('An error occurred while connecting to the database');
    }
});

app.get('/test-connection2', (req, res) => {
    res.send('hello world')
});

app.patch('/update_guessed/:word', async (req, res) => {
    const { word } = req.params;
    // const { times_guessed } = req.body;
    const sql = 'UPDATE words_english SET times_guessed = COALESCE(times_guessed, 0) + 1 WHERE word = $1';
    try {
        const result = await pool.query(sql, [word]);
        res.json(result.rows);
    } catch (err) {
        console.error(err.message);
    }
});

app.patch('/update_not_guessed/:word', async (req, res) => {
    const { word } = req.params;
    // const { times_guessed } = req.body;
    const sql = 'UPDATE words_english SET times_not_guessed = COALESCE(times_not_guessed, 0) + 1 WHERE word = $1';
    try {
        const result = await pool.query(sql, [word]);
        res.json(result.rows);
    } catch (err) {
        console.error(err.message);
    }
});

app.get('/stats', async (req, res) => {
    // const { word } = req.params;
    // const { times_guessed } = req.body;
    const sql = 'SELECT word,times_guessed, times_not_guessed, (times_guessed*100/(times_guessed+times_not_guessed)) as total FROM words_english ORDER BY total DESC ';
    try {
        const result = await pool.query(sql);
        res.json(result.rows);
    } catch (err) {
        console.error(err.message);
    }
});

app.listen(7055, () => {
    console.log('API server listening on port 7055');
});