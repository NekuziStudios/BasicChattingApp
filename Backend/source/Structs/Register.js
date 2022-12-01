function DoesEmailExist(email) {
    var bCanContinue = true;
    const AllProfiles = require('../databases/users/users.json');

    AllProfiles.forEach(user => {
        if(email == user.email) {
            bCanContinue = false;
        }
    })

    if(bCanContinue == true) {
        return true;
    } else {
        return false;
    }
}

function DoesUsernameExist(username) {
    var bCanContinue = true;
    const AllProfiles = require('../databases/users/users.json');

    AllProfiles.forEach(user => {
        if(username == user.username) {
            bCanContinue = false;
        }
    })

    if(bCanContinue == true) {
        return true;
    } else {
        return false;
    }
}


module.exports = {
    DoesEmailExist,
    DoesUsernameExist
}