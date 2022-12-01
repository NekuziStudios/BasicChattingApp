var authType = {
    NO_AUTH: 0,
    EMAILPASS: 1,
    PHONE: 2,
    MAX: 3
}

var permType = {
    NONE: 0,
    NORMAL: 1,
    MOD: 2,
    ADMIN: 3,
    OWNER: 4
}

var messageType = {
    NO_CONTENT: 0,
    NORMAL: 1,
    PING: 2,
    BOLD: 3,
    ITALIC: 4,
    LIGHT: 5,
    COLOR: 6,
    CODESTRIP: 7
}

var adminType = {
    TRAINING: 0,
    PART: 1,
    FULL: 2
}

var subscriptionType = {
    NONE: 0,
    VIP: 1
}

var commandType = {
    BAN: 0,
    KICK: 1,
    REMOVE: 2,
    MUTE: 3,
    CHANGENICKNAME: 5
}

module.exports = {
    authType,
    permType,
    messageType,
    adminType,
    subscriptionType,
    commandType
}