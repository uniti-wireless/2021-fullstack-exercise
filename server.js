const express = require("express");
const app = express();

let port = 8080;

app.get('/hello', (req, res) => {
    res.send('hello world: nodemon restart working')
})

app.listen(port, () => {
    console.log(`Now listening on port: ${port}`)
})