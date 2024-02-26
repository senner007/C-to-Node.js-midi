const path = require('path');

var namespace = 'QuickStart.Core'

var version = '6.0-windows10.0.19041.0'

const baseNetAppPath = path.join(__dirname, '/src/'+ namespace +'/bin/Debug/net'+ version);

process.env.EDGE_USE_CORECLR = 1;

var edge = require('edge-js');

var baseDll = path.join(baseNetAppPath, namespace + '.dll');

var localTypeName = namespace + '.LocalMethods';


var midiHandle = edge.func({
    assemblyFile: baseDll,
    typeName: localTypeName,
    methodName: 'PlayMidi'
});

midiHandle({ openMidi : true }, function(error, result) {
    if (error) throw error;
    console.log(result);

    
    midiHandle({ delay: 2000, note : 70, isOn: true}, function(error, result) {
        if (error) throw error;
        console.log(result);
    });

    setTimeout(() => {
        midiHandle({ delay: 0, note : 70, isOn : false}, function(error, result) {
            if (error) throw error;
            console.log(result);
        });

        midiHandle({ delay: 2000, note : 80, isOn : true}, function(error, result) {
            if (error) throw error;
            console.log(result);
        });

        midiHandle({ delay: 2000, note : 50, isOn : true}, function(error, result) {
            if (error) throw error;
            console.log(result);
        });
    }, 500)


});


