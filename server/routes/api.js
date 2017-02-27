var Joi = require('joi');
var _ = require('underscore');
var uuid = require('uuid/v4');
var express = require('express');
var router = express.Router();

var schema = require('../schema');
var log = require('../middlewares/log');
var login = require('../middlewares/login');
var docClient = require('../awsclient').docClient;

router.post('/*', log);

router.post('/RegisterUser', function(req, res) {
    const result = Joi.validate(req.body, schema.RegisterUser);
    if(result.error !== null){
        res.send('error');
        return;
    }
    var body = result.value;
    var obj = {
        "PlayerId": uuid(),
        "Email": body.email,
        "Password": body.password,
        "Created": (new Date()).toJSON()
    };
    var params = {
        TableName:"UserAccounts",
        Item:obj
    };
    console.log(obj.PlayerId);
    docClient.put(params, function(err, data) {
        if (err) {
            console.error("Unable to add item. Error JSON:", JSON.stringify(err, null, 2));
        } else {
            console.log("Added item:", data.email);
            console.log("Added item:", JSON.stringify(data, null, 2));
        }
    });
    res.send('succses' + result.value);
});

router.post('/LoginUser', function(req, res) {
    const result = Joi.validate(req.body, schema.LoginUser);
    if(result.error !== null){
        console.log(result.error);
        res.send('error');
        return;
    }
    var body = result.value;
    var params = {
        TableName: "UserSessions",
        Item:{
            "SessionId": uuid(),
            "PlayerId": body.PlayerId,
            "Created": new Date()
        }
    };

    docClient.put(params, function(err, data) {
        if (err) {
            res.send('succses' + result.value);
        } else {
            res.json(_.pick(params.Item, 'SessionId', 'PlayerId'));
        }
    });
});

module.exports = router;
