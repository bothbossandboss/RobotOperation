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

            SmallWalk smallWalk = new SmallWalk();
            ServoManager servoManager = new ServoManager();
            string nowPos = "posFirst";
            Dictionary<ServoTag, double> preD;
            Dictionary<ServoTag, double> nextD;

            while(true) {
                switch (nowPos) {
                    case "posFirst":
                        preD = smallWalk.posDic["posFirst"];
                        nextD = smallWalk.posDic["pos13"];
                        break;
                }

            }


        }

    }
}
/*
*/