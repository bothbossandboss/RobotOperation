/**
 * アプリケーションのメイン関数を含むクラス。
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotOperation {
    static class Program {
        [STAThread]
        static void Main() {
            Console.WriteLine("Hello World using C#!");
            ServoManager servoManager = new ServoManager();
            byte[] value1 = servoManager.generateCommand();
            servoManager.setPos0();
            byte[] value2 = servoManager.generateCommand();
        }
    }
}
