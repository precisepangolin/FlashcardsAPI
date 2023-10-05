//const { "ep-frosty-firefly-23612795.eu-central-1.aws.neon.tech","Flashcards", 'charls7c', 'r3Y97kyZ.KE5NqA','ep-frosty-firefly-23612795'} = process.env;

// app.js
const express = require('express');
const { Pool } = require('pg')
const cors = require('cors');
const bodyParser = require('body-parser'); // Add this import
const axios = require('axios');
const jwt = require('jsonwebtoken');

const JWT_SECRET = 'your-secret-key';

const verifyToken = (req, res, next) => {
    const token = req.header('Authorization');

    if (!token) {
        return res.status(401).json({ message: 'Authorization token missing' });
    }

    jwt.verify(token, JWT_SECRET, (err, decoded) => {
        if (err) {
            return res.status(401).json({ message: 'Invalid token' });
        }

        // Attach the decoded user information to the request object
        req.user = decoded;
        next();
    });
};

//const URL = `postgres://charls7c:r3Y97kyZ.KE5NqA@ep-frosty-firefly-23612795.eu-central-1.aws.neon.tech/Flashcards?options=project%3Dep-frosty-firefly-23612795`;
const app = express();
app.use(bodyParser.json()); // Parse JSON requests

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

const newPool = new Pool({
    host: 'ep-icy-mode-56066025.eu-central-1.aws.neon.tech',
    user: 'Ianzev',
    password: 'vYjXz8qPnLu6',
    database: 'UsersRegistered',
    ssl: {
        rejectUnauthorized: false,
        sslmode: 'require'
    }})

app.post('/register', async (req, res) => {
    const { username, password, name, lastName, email } = req.body;
    try {
        const checkQuery = 'SELECT * FROM usersregistered WHERE username = $1';
        const { rows: existingUsers } = await newPool.query(checkQuery, [username]);

        if (existingUsers.length > 0) {
            return res.status(400).json({ message: 'Username already in use' });
        }
        
        const insertQuery = `
            INSERT INTO usersregistered (username, password_hash, name, lastname, email)
            VALUES ($1, $2, $3, $4, $5)`;

        const { rows: newUser } = await newPool.query(insertQuery, [username, password, name, lastName, email]);
        
        res.status(201).json({ message: 'Registration successful', user: newUser[0] });
    } catch (error) {
        console.error('Error during registration:', error);
        res.status(500).json({ error: 'Internal server error' });
    }
});

app.post('/login', async (req, res) => {
    const { username, password } = req.body;
    try {
        // Query the database to check if the username exists
        const checkQuery = 'SELECT * FROM usersregistered WHERE username = $1';
        const { rows: existingUsers } = await newPool.query(checkQuery, [username]);

        if (existingUsers.length === 0) {
            // Username does not exist
            return res.status(401).json({ message: 'Username not found' });
        }

        // Check if the provided password matches the stored password_hash
        const user = existingUsers[0];
        if (password !== user.password_hash) {
            // Passwords do not match
            return res.status(401).json({ message: 'Incorrect password' });
        }
        const token = jwt.sign({ userId: user.user_id, username: user.username }, 'your-secret-key', {
            expiresIn: '1h', // Token expiration time
        });
        const userDetails = {
            name: user.name,
            lastName: user.lastname,
            email: user.email,
            username: user.username
            // Add other user details as needed
        };
        res.status(200).json({ message: 'Login successful', token, user: userDetails });
    } catch (error) {
        console.error('Error during login:', error);
        res.status(500).json({ error: 'Internal server error' });
    }
});

app.get('/profile', verifyToken, async (req, res) => {
    // Assuming `req.user` contains the decoded user information from the token
    const userId = req.user.userId; // Adjust this according to your token structure

    try {
        // Query the database to fetch user details based on `userId`
        const profileQuery = 'SELECT name, lastname, email, username FROM usersregistered WHERE user_id = $1';
        const { rows: userProfile } = await newPool.query(profileQuery, [userId]);

        if (userProfile.length === 0) {
            return res.status(404).json({ message: 'User not found' });
        }

        res.status(200).json({ message: 'Profile details retrieved successfully', profile: userProfile[0] });
    } catch (error) {
        console.error('Error fetching user profile:', error);
        res.status(500).json({ error: 'Internal server error' });
    }
});

app.get('/words', async (req, res) => {
    try {
        const result = await pool.query('SELECT word,hint FROM Words_english ORDER BY RANDOM() LIMIT 1');
        const wordToTranslate = result.rows[0].word; // Get the word from the response

        // const encodedParams = new URLSearchParams();
        // encodedParams.set('q', wordToTranslate);
        // encodedParams.set('target', 'pl');
        //
        // const options = {
        //     method: 'POST',
        //     url: 'https://google-translate1.p.rapidapi.com/language/translate/v2/',
        //     headers: {
        //         'content-type': 'application/x-www-form-urlencoded',
        //         'Accept-Encoding': 'application/gzip',
        //         'X-RapidAPI-Key': '90b47d55d9mshf1a7e11e45ee0b1p1972acjsndd8b1439e7bc',
        //         'X-RapidAPI-Host': 'google-translate1.p.rapidapi.com'
        //     },
        //     data: encodedParams.toString(),
        // };
        //
        // const response = await axios.request(options);
        //
        // result.rows[0].translation = response.data.data.translations[0].translatedText;
        // // console.log(result.rows[0].word);
        // // console.log(response.data);
        // console.log(response.data.data.translations[0].translatedText)
        res.json(result.rows)
    } catch (error) {
        console.error(error);
    }
});

app.get('/test-new-database-connection', async (req, res) => {
    try {
        // Use newPool.query to perform a simple query to the new database
        const result = await newPool.query('SELECT NOW()');
        res.send('Connection to the new database successfu1');
    } catch (error) {
        console.error(error);
        res.status(500).send('An error occurred while connecting to the new database');
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