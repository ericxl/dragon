var AWS = require('aws-sdk');
AWS.config.update({
    region: "us-west-2",
    endpoint: "http://localhost:8000"
});

module.exports = {
    dynamoDb: AWS.DynamoDB,
    docClient: new AWS.DynamoDB.DocumentClient()
};
