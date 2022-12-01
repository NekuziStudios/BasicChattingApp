
const fs = require('fs');
const path = require('path');

// only need createUser for now
function CreateUser(username, password, email, isVerified) {
    var UserJson = {
        "username": username,
        "password": password,
        "email": email,
        "isVerified": isVerified,
        "isBanned": false,
        "isMuted": false,
        "level": 0,
        "messages": []
    }
    const allProfiles = require('../databases/users/users.json');
    allProfiles.push(UserJson); // push profile to user database
    var allProfilesNew = JSON.stringify(allProfiles, null, 2)


    fs.writeFileSync(path.join(__dirname, '../databases/users/users.json'), allProfilesNew); // write new version of database to existing 
    console.log('successfully created new profile');
}

function FindUser(email) {
    var bCanContinue = false;
    var Index = 0;
    const allProfiles = require('../databases/users/users.json');

    allProfiles.forEach((user, index) => {
        if (email == user.email) {
            bCanContinue = true;
            Index = index;
        }
    })

    if (!bCanContinue == true) return JSON.stringify({ "err": true }, null, 2);

    if (Index != -1) {
        return JSON.stringify({ "err": false }, null, 2);
    } else {
        console.log('cannot get index!');
        return JSON.stringify({ "err": true }, null, 2);
    }
}

function LoginUser(email, password) {
    var bCanContinue = false;
    var Index = 0;
    const allProfiles = require('../databases/users/users.json');

    allProfiles.forEach((user, index) => {
        if (email == user.email && password == user.password) {
            bCanContinue = true;
            Index = index;
        }
    })

    if (!bCanContinue == true) return JSON.stringify({ "err": true }, null, 2);

    if (Index != -1) {
        return allProfiles[Index];
    } else {
        console.log('Cannot get index');
    }
}

function IsUserEligbleForNewLevel(username) {
    const xpdatabase = require('../databases/xp/xp.json');
    var LevelXP = 0; // the xp in a account
    var last_levelcount = 0; // the last time the server has congratulated for level
    var Exists = false; // if profile exists

    xpdatabase.forEach((xp, index) => {
        if (username == xp.username) {
            LevelXP = xp.xpcount;
            last_levelcount = xp.level_count;
            Exists = true;
        }
    });

    if (Exists != true) return JSON.stringify({ "no_level": true });

    const leveldatabase = require('../databases/levels/levelcounts.json');

    var newLevel = false; // if there is a new level
    var Level = 0; // Level
    leveldatabase.forEach((level, index) => {
        if (level.xprequired >= LevelXP) {
            if (last_levelcount >= level.level) {
                newLevel = true;
                Level = level.level;
            }
        }
    });

    if (newLevel != true) return JSON.stringify({ "no_level": true });

    var LevelJSON = {
        "new_level": Level,
        "username": username,
        "no_level": false
    }

    return JSON.stringify(LevelJSON);
}

function AwardUserXP(username, xpcount) {
    // see if player exist in xp database
    const xpdatabase = require('../databases/xp/xp.json');
    var bUserExistsInXp = false;
    var bUserIndex = 0; // xpdatabase index if exist
    xpdatabase.forEach((xpfriend, index) => {
        if (xpfriend.username == username) {
            bUserExistsInXp = true;
            bUserIndex = index;
        }
    })

    // whole writing of the code
    if (bUserExistsInXp == true) {
        if (bUserIndex != -1) {
            xpdatabase[bUserIndex].xpcount = xpdatabase[bUserIndex].xpcount + xpcount;
            var xpDatabaseNew = JSON.stringify(xpdatabase, null, 2)
            fs.writeFileSync(path.join(__dirname, '../databases/xp/xp.json'), xpDatabaseNew);
        }
    } else {
        var newXpEntry = {
            "username": username,
            "xpcount": xpcount,
            "level_count": 0
        }
        xpdatabase.push(newXpEntry);
        var xpDatabaseNew = JSON.stringify(xpdatabase, null, 2)
        fs.writeFileSync(path.join(__dirname, '../databases/xp/xp.json'), xpDatabaseNew);
    }

    // redo it again to check if it exists now
    xpdatabase.forEach((xpfriend, index) => {
        if (xpfriend.username == username) {
            bUserExistsInXp = true;
            bUserIndex = index;
        }
    })

    if (bUserExistsInXp == true) {
        var newLevel = false;
        var LevelCount = 0; // the new level
        const Levels = require('../databases/levels/levelcounts.json');

        Levels.forEach(level => {
            if (xpdatabase[bUserIndex].xpcount >= level.xprequired) {
                LevelCount = level.level;
                newLevel = true;
            }
        })

        if (newLevel == true) {
            const AllProfiles = require('../databases/users/users.json');
            AllProfiles.forEach((user, index) => {
                if (user.username == username) {
                    user.level = LevelCount;
                }
            })

            var AllprofilesNew = JSON.stringify(AllProfiles, null, 2)
            fs.writeFileSync(path.join(__dirname, '../databases/users/users.json'), AllprofilesNew);
        } else {
            return;
        }
    } else {
        console.log('There was an error writing new entry'); // there was a obvious error
    }
}

function DeleteUser(username, index) {

}

function UpdateUser() {

}

function BanUser() {

}

function KickUser() {

}

function RemoveUser() {

}

function MuteUser(username) {
    const allProfiles = require('../databases/users/users.json');
    var UserFound = false;
    var msg;
    allProfiles.forEach((user, index) => {
        console.log("if " + user.username + " = " + username);
        if (user.username == username || user.username + " " == username || " " + user.username == username) {
            UserFound = true;
            user.isMuted = true;
            console.log(user.username);
        }
    });

    if(!UserFound == true) {
        var msg = {
            "success": false,
            "reason": "user_not_found"
        }
        return msg;
    }

    var AllprofilesNew = JSON.stringify(allProfiles, null, 2)
    fs.writeFileSync(path.join(__dirname, '../databases/users/users.json'), AllprofilesNew);

    var msg = {
        "success": true
    }
    return msg;
}

function IsUserMuted(username) {
    
}

function CreateToken() {

}

function RevokeToken() {

}

module.exports = {
    CreateToken,
    RevokeToken,
    MuteUser,
    RemoveUser,
    KickUser,
    BanUser,
    UpdateUser,
    DeleteUser,
    CreateToken,
    CreateUser,
    FindUser,
    LoginUser,
    AwardUserXP,
    IsUserEligbleForNewLevel
}