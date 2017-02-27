var log = function(req, res, next){
    console.log('getting request router on path: ', req.path.toLowerCase());
    next();
};
module.exports = log;
