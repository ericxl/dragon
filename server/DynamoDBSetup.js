var dynamodb = require('./awsclient').dynamoDb;

dynamodb.deleteTable({TableName : "UserAccounts"}, function(err, data) {
    var userAccountsParams = {
        TableName : "UserAccounts",
        KeySchema: [
            { AttributeName: "PlayerId", KeyType: "HASH"}
        ],
        AttributeDefinitions: [
            { AttributeName: "PlayerId", AttributeType: "S" }
        ],
        ProvisionedThroughput: {
            ReadCapacityUnits: 10,
            WriteCapacityUnits: 10
        }
    };

    dynamodb.createTable(userAccountsParams, function(err, data) {
        if (err) {
            console.error("Unable to create table. Error JSON:", JSON.stringify(err, null, 2));
        } else {
            console.log("Created table. Table description JSON:", JSON.stringify(data, null, 2));
        }
    });
});
dynamodb.deleteTable({TableName : "UserSessions"}, function(err, data) {
    var userSessionsParams = {
        TableName : "UserSessions",
        KeySchema: [
            { AttributeName: "SessionId", KeyType: "HASH"}
        ],
        AttributeDefinitions: [
            { AttributeName: "SessionId", AttributeType: "S" }
        ],
        ProvisionedThroughput: {
            ReadCapacityUnits: 10,
            WriteCapacityUnits: 10
        }
    };

    dynamodb.createTable(userSessionsParams, function(err, data) {
        if (err) {
            console.error("Unable to create table. Error JSON:", JSON.stringify(err, null, 2));
        } else {
            console.log("Created table. Table description JSON:", JSON.stringify(data, null, 2));
        }
    });
});
