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
            Application.Run(new Form1());
//            SerialPortManager serialPortManager = new SerialPortManager("COM5"); //ロボットの接続場所に依存
 //           serialPortManager.Open();

            SmallWalk smallWalk = new SmallWalk();
            ServoManager servoManager = new ServoManager();
        }
    }
}
/*
*/