using System;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.Midi;

namespace QuickStart.Core
{
    public class LocalMethods
    {
        public IMidiOutPort currentMidiOutputDevice { get; set; } = null;

        public async Task<string> OpenMidi()
        {
            var deviceList = await DeviceInformation.
                FindAllAsync(MidiOutPort.GetDeviceSelector());
            
            var deviceName = "";

            foreach (var deviceInfo in deviceList)
            {
                deviceName = deviceInfo.Name.ToString();
                currentMidiOutputDevice = await MidiOutPort.FromIdAsync(deviceInfo.Id);

            }
            return "Midi Opened: " + deviceName;
        }
        public async Task<object> PlayMidi(dynamic input)
        {
            
            if (currentMidiOutputDevice == null && (bool)input.openMidi)
            {
                var midiIsOpen = await OpenMidi();
                return midiIsOpen;
            }

            int delay = (int)input.delay;
            int note = (int)input.note;
            bool isOn = (bool)input.isOn;


            if (isOn == true) {
                var midiMessageToSend = new MidiNoteOnMessage(Convert.ToByte(0), Convert.ToByte(note), Convert.ToByte(127));
                currentMidiOutputDevice.SendMessage(midiMessageToSend);
                await Task.Delay(delay);
            } else {
                var midiMessageToSend = new MidiNoteOffMessage(Convert.ToByte(0), Convert.ToByte(note), Convert.ToByte(127));
                currentMidiOutputDevice.SendMessage(midiMessageToSend);
            }

            return "Note played";


        }
    }
}
