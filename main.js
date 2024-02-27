const path = require('path');
var namespace = 'QuickStart.Core'
var version = '6.0-windows10.0.19041.0'
const baseNetAppPath = path.join(__dirname, '/src/' + namespace + '/bin/Debug/net' + version)
process.env.EDGE_USE_CORECLR = 1;
var edge = require('edge-js');
var baseDll = path.join(baseNetAppPath, namespace + '.dll');
var localTypeName = namespace + '.LocalMethods';

// Read Assembly
var midiHandle = edge.func({
    assemblyFile: baseDll,
    typeName: localTypeName,
    methodName: 'PlayMidi'
});

// Logs exception : 'Midi not open. Please call with param: { openMidi : true }'
try {
    midiHandle({ delay: 2000, note: 70, isOn: true }, function (error, result) {
        if (error) throw error;
        console.log(result);
    });

} catch (e) {
    console.log('.NET Exception: ' + e.Message);
}


// Await device select and add to midi out port
midiHandle({ openMidi: true }, function (error, result) {
    if (error) throw error;
    console.log(result);

    // Play note
    midiHandle({ delay: 2000, note: 70, isOn: true }, function (error, result) {
        if (error) throw error;
        console.log(result);
    });

    setTimeout(() => {
        // Stop note
        midiHandle({ delay: 0, note: 70, isOn: false }, function (error, result) {
            if (error) throw error;
            console.log(result);
        });

        // Play notes
        midiHandle({ delay: 2000, note: 80, isOn: true }, function (error, result) {
            if (error) throw error;
            console.log(result);
        });

        midiHandle({ delay: 2000, note: 50, isOn: true }, function (error, result) {
            if (error) throw error;
            console.log(result);
        });
    }, 500)

});


