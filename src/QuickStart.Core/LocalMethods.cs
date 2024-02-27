using System;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.Midi;

namespace QuickStart.Core
{
    public class LocalMethods
    {
        public IMidiOutPort CurrentMidiOutputDevice { get; set; } = null;

        public readonly int VOLUME = 127;
        public readonly int CHANNEL = 0;

        public async Task<string> OpenMidi()
        {
            var deviceList = await DeviceInformation.
                FindAllAsync(MidiOutPort.GetDeviceSelector());

            var deviceName = "";

            foreach (var deviceInfo in deviceList)
            {
                deviceName = deviceInfo.Name.ToString();
                CurrentMidiOutputDevice = await MidiOutPort.FromIdAsync(deviceInfo.Id);

            }
            return "Midi Opened: " + deviceName;
        }
        public async Task<object> PlayMidi(dynamic input)
        {

            if (CurrentMidiOutputDevice == null)
            {

                try
                {
                    if ((bool)input.openMidi == true)
                    {
                        return await OpenMidi();
                    }
                }
                catch
                {
                    throw new Exception("Midi not open. Please call with param: { openMidi : true }");
                }

            }


            int delay = (int)input.delay;
            int note = (int)input.note;
            bool isOn = (bool)input.isOn;

            IMidiMessage midiMessage = null;

            if (isOn == true)
            {
                midiMessage = new MidiNoteOnMessage(Convert.ToByte(CHANNEL), Convert.ToByte(note), Convert.ToByte(VOLUME));
            }
            else
            {
                midiMessage = new MidiNoteOffMessage(Convert.ToByte(CHANNEL), Convert.ToByte(note), Convert.ToByte(VOLUME));
            }

            CurrentMidiOutputDevice.SendMessage(midiMessage);
            
            await Task.Delay(isOn ? delay : 0);

            return isOn ? "Note played: " + note : "Note Stopped: " + note;

        }
    }
}
