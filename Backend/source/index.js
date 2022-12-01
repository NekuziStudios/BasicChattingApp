const express = require('express');
const fs = require('fs');
const app = express();
const path = require('path');
const http = require('http');
const web = require('websocket').server;
const server2 = http.createServer().listen(3300);
const websocket = new web({
    httpServer: server2
});
var swearWords = ["nigger", "nigga", "cunt", "slut", "whore", "hitler", "core", "coremp", "corefn", "skid", "nuke", "raid", "shit", "dick", "ass", "jackass", "retard", "scum", "dumbass", "fool", "dox", "swat", "pizza bomb", "kys", "kms", "kill", "fuck", "bitch", "porn", "gore"];


console.clear();

app.use(express.json())
app.use(express.urlencoded({ extended: true }))

app.get('/api/login', (req, res) => {
    let email = req.query.email;
    let password = req.query.password;
    let FindProfile = require('./Structs/User').FindUser(email);

    if (FindProfile.err == true) return res.json({ "err": true, "code": "profile_not_found" });
    let Profile = require('./Structs/User').LoginUser(email, password);
    if (Profile.err) return res.json({ "err": true, "code": "password_email_failure" });
    return res.json(Profile);
});

app.post('/api/register', (req, res) => {
    console.log(req.body);
    var registerfunc = require('./Structs/Register');
    var UserStructs = require('./Structs/User');
    var bcancontinue1 = registerfunc.DoesEmailExist(req.body.email);
    var bcancontinue2 = registerfunc.DoesUsernameExist(req.body.username);

    if (!bcancontinue1 == true) return res.send('Email already exists');
    if (!bcancontinue2 == true) return res.send('Username already exists');

    UserStructs.CreateUser(req.body.username, req.body.password, req.body.email, false); // Creates user
    return res.send('Account created!');
});

app.get('/register', (req, res) => {
    res.sendFile(path.join(__dirname, './views/register.html'));
});

app.get('/', (req, res) => {
    return res.json({ "operator": "com_johnshier", "message": "your not supposed to be here <3" });
});

// start Webserver
app.listen(require('./config.json').webPort, () => {
    console.log('Webserver online!');
});

// Chat servers
var clients = new Array();
websocket.on("request", req => {
    const cli = req.accept(null, req.origin);
    clients.push(cli);

    cli.on("close", () => {
        clients.splice(clients.indexOf(cli), 1);
    });

    cli.on("message", msg => {
        const message = msg.utf8Data;
        const json = JSON.parse(message);
        
        // cmd stuff
        if(json.cmd == "c_new_user") {
            var newMember;
            if(json.user == require('./config.json').ownerName) {
                newMember = {
                    "cmd": "cl_new_member_msg",
                    "user": "♛ " + json.user
                }
            } else {
                newMember = {
                    "cmd": "cl_new_member_msg",
                    "user": json.user
                }
            }

            websocket.broadcastUTF(JSON.stringify(newMember));
        } else if(json.cmd == "c_new_message_normal") {
            var newMessage;
            var censorMessage = false;
            var MuteCmdExecuted = false;

            swearWords.forEach(swear => {
                if(json.content == swear) {
                    censorMessage = true;
                }
            });

            if(json.content.includes("/mute")) {
                console.log('mute command executed');
                MuteCmdExecuted = true;
                var username = json.content.slice(0, json.content.indexOf('/'));
                var MuteUserFunc = require('./Structs/User').MuteUser(username);

                if(MuteUserFunc.success != true) {
                    if(MuteUserFunc.reason == "user_not_found") {
                        newMessage = {
                            "cmd": "cl_append_msg",
                            "author": "凹[◎凸◎]凹 SERVER",
                            "content": "Failed to mute " + username + ", user not found!"
                        }
                    }
                } else {
                    newMessage = {
                        "cmd": "cl_append_msg",
                        "author": "凹[◎凸◎]凹 SERVER",
                        "content": "successfully muted " + username + "!"
                    }
                }
                
            } else {
                if(censorMessage == true) {
                    newMessage = {
                        "cmd": "cl_append_msg",
                        "author": "|｡_｡| SERVER",
                        "content": "Please remember to keep Chatting app friendly " + json.author + "!"
                    }
                } else {
                    if(json.author == require('./config.json').ownerName) {
                        newMessage = {
                            "cmd": "cl_append_msg",
                            "author": "♛ " + json.author,
                            "content": json.content
                        }
                    } else {
                        newMessage = {
                            "cmd": "cl_append_msg",
                            "author": json.author,
                            "content": json.content
                        }
                    }
                }
            }

            
            if(json.content != 0 || MuteCmdExecuted == true) {
                websocket.broadcastUTF(JSON.stringify(newMessage)); // Message to share
            }
        } else if(json.cmd == "c_ban_user") {

        }
    });
});