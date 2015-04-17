/**
 * KHR-3HVとシリアル通信するためのクラス。
 * インスタンス生成時にポートの名前(COM5など)を指定。
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using Rcb4;

namespace RobotOperation {
    class SerialPortManager {
        private SerialPort MyPort;

        public SerialPortManager(string portName) {
            MyPort = new SerialPort(portName, 115200, Parity.Even, 8, StopBits.One);
            MyPort.ReadTimeout = 1000;
        }

        public string Open() {
            try {
                if (!MyPort.IsOpen) {
                    MyPort.Open();
                }
            }catch (Exception e) {
                return e.Message;
            }
            return "connected!";
        }

        public void sendMessage(byte[] msg) {
            Console.WriteLine(BitConverter.ToString(msg));
            if (MyPort.IsOpen) {
                //rxは受信用変数。マニュアルによると4バイトのデータらしい。
                //RCB-4コマンドリファレンスP.18,19を参照。
                byte[] rx = new byte[4];
                //ref は参照渡しを意味する。
                Command.Synchronize(MyPort, msg, ref rx);
            }
        }

    }
}
