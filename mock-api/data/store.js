"use strict";
exports.__esModule = true;
exports.listClients = exports.removeClient = exports.updateClient = exports.addClient = exports.store = void 0;
exports.store = {
    entities: {
        'xx-aa-bb': {
            id: 'xx-aa-bb',
            firstName: 'John',
            lastName: 'Smitherin',
            email: 'john@gmail.com',
            phoneNumber: '+6192099102'
        }
    }
};
var addClient = function (client) {
    exports.store.entities[client.id] = client;
};
exports.addClient = addClient;
var updateClient = function (client) {
    exports.store.entities[client.id] = client;
};
exports.updateClient = updateClient;
var removeClient = function (id) {
    delete exports.store.entities[id];
};
exports.removeClient = removeClient;
var listClients = function () {
    var list = Object.keys(exports.store.entities).map(function (id) { return exports.store.entities[id]; });
    return list.sort(function (a, b) {
        if (a.firstName < b.firstName) {
            return -1;
        }
        if (a.firstName > b.firstName) {
            return 1;
        }
        return 0;
    });
};
exports.listClients = listClients;
