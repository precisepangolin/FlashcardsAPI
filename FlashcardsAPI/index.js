//const { "ep-frosty-firefly-23612795.eu-central-1.aws.neon.tech","Flashcards", 'charls7c', 'r3Y97kyZ.KE5NqA','ep-frosty-firefly-23612795'} = process.env;

// app.js
const express = require('express');
const { Pool } = require('pg')


//const URL = `postgres://charls7c:r3Y97kyZ.KE5NqA@ep-frosty-firefly-23612795.eu-central-1.aws.neon.tech/Flashcards?options=project%3Dep-frosty-firefly-23612795`;
const app = express();
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
        const result = await pool.query('SELECT * FROM persons');
        
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

app.listen(7035, () => {
    console.log('API server listening on port 7035');
});