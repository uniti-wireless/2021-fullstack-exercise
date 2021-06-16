const express = require("express");
const app = express();
require('dotenv').config();
const yaml = require('js-yaml');
const fs = require('fs');
const jwt = require('jsonwebtoken');
app.use(express.json());
app.use(express.urlencoded({ extended: true }));

let port = 8080;

app.get('/api', (req, res) => {
    res.send('Welcome. api is functioning correctly')
})

app.post('/api/data', authToken, (req, res) => {
    jwt.verify(req.token, process.env.JWT_SECRET, (err, authData) => {
        if(err) {
            res.sendStatus(403)
        } else {
            let fileContents = fs.readFileSync('./exercise.yaml', 'utf8');
            let data = yaml.load(fileContents);
            let customersData = data.customers

            res.json({
                customersData
            })
        }
    })
})

app.post('/api/login', authUser, (req, res) => {
    let user = req.body

    jwt.sign({ user }, process.env.JWT_SECRET, (err, token) => {
        res.json({ token })
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

function authUser(req, res, next) {
    let fileContents = fs.readFileSync('./exercise.yaml', 'utf8');
    let data = yaml.load(fileContents);
    
    let user = req.body
    let result = data.auth.filter(loginDetails => {
        return loginDetails.password === user.password && loginDetails.username === user.username
    })

    if(result.length > 0) {
        next()
    } else {
        res.sendStatus(403)
    }
}

app.listen(port, () => {
    console.log(`Now listening on port: ${port}`)
})