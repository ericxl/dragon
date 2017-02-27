/**
 * Created by Eric on 2/26/2017.
 */
var Joi = require('joi');

var registerUser = Joi.object().keys({
    Username: Joi.string().alphanum().min(6).max(30),
    Password: Joi.string().regex(/^[a-zA-Z0-9]{3,30}$/).required(),
    Email: Joi.string().email()
}).with('Email', 'Password');
var loginUser = Joi.object().keys({
    Password: Joi.string().regex(/^[a-zA-Z0-9]{3,30}$/).required(),
    Email: Joi.string().email()
}).with('Email', 'Password');
module.exports = {
    RegisterUser : registerUser,
    LoginUser : loginUser,
};
