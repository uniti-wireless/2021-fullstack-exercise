const express = require("express");
const app = express();
const yaml = require('js-yaml');
const fs = require('fs');
const jwt = require('jsonwebtoken');
const { json } = require("express");

let port = 8080;

app.get('/hello', (req, res) => {
    res.send('hello world: nodemon restart working')
})

app.get('/test', (req, res) => {
    try {
        let fileContents = fs.readFileSync('./exercise.yaml', 'utf8');
        let data = yaml.load(fileContents);

        console.log(data);
    } catch (e) {
        console.log(e);
    }

    res.send('test route for reading .yaml file. data console logged to server')
})

app.post('/api/data', authToken, (req, res) => {
    jwt.verify(req.token, 'secretkey', (err, authData) => {
        if(err) {
            res.sendStatus(403)
        } else {
            res.json({
                message: 'api data route',
                authData
            })
        }
    })
})

function authToken(req, res, next) {
    const bearerHeader = req.headers['authorization']
    if (typeof bearerHeader !== 'undefined') {
        const bearerToken = bearerHeader.split(' ')[1]
        req.token = bearerToken
        next()
    } else {
        res.sendStatus(403)
    } 
}  

app.post('/api/login', (req, res) => {
    const auth = {
        username: 'john',
        password: 'password123'
    }

    jwt.sign({ auth: auth }, 'secretkey', (err, token) => {
        res.json({ token })
    })
})

app.listen(port, () => {
    console.log(`Now listening on port: ${port}`)
})