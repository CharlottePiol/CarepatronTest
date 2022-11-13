var store = require('../mock-api/data/store');
var EventEmitter = require('events');
var emt = new EventEmitter();

// emt.on('addClient', function() {
//   console.log('an event occurred!');
// });

// emt.emit('addClient');

emt.on(store.addClient, (arg) => {
    console.log('create event occurred!', arg);
});

emt.on(store.updateClient, (arg) => {
    console.log('update event occurred!', arg);
});


emt.on(store.removeClient, (arg) => {
    console.log('remove event occurred!', arg);
});

  
emt.emit(store.addClient, { 
    id: 'xx-aa-cc',
    firstName: 'Johnsss',
    lastName: 'Smitherinsss',
    email: 'john@gmail.com',
    phoneNumber: '+6192099102'
});

emt.emit(store.updateClient, { 
    id: 'xx-aa-bb',
    firstName: 'Johnaaa',
    lastName: 'Smitherinaaa',
    email: 'john@gmail.com',
    phoneNumber: '+6192099102'
});
  
  

console.log(">>>" + JSON.stringify(store.store));

