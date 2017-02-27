var express    = require('express');        // call express
var app        = express();                 // define our app using express
var bodyParser = require('body-parser');
var api = require('./routes/api');

app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());

app.use('/api', api);

var port = process.env.PORT || 3000;        // set our port

app.listen(port, function(){
    console.log('Start listening on port ' + port);
});
