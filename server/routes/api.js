var express = require('express');
var router = express.Router();

router.post('/*', function(req, res, next){
  console.log('getting request router on path: ', req.path.toLowerCase());
  next();
});

router.post('/LoginWithIOSDeviceID', function(req, res) {
    res.send('LoginWithIOSDeviceID with a resource');
});

router.post('/LoginWithAndroidDeviceID', function(req, res) {
    res.send('LoginWithAndroidDeviceID with a resource');
});

module.exports = router;
