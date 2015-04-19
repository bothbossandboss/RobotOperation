using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotOperation {
    public partial class Form1 : Form {
        private SerialPortManager serialPortManager;
        private SmallWalk smallWalk;
        private ServoManager servoManager;

        public Form1() {
            InitializeComponent();
            serialPortManager = new SerialPortManager("COM3");
            smallWalk = new SmallWalk();
            servoManager = new ServoManager();
        }

        private void Form1_Load(object sender, EventArgs e) {
            //kinectの初期化とか書くか。
        }

        private void connectButtonClicked(object sender, EventArgs e) {
            Console.WriteLine("connect");
            serialMessage.Text = "connecting...";
            serialMessage.Text = serialPortManager.Open();
        }

        private void startButtonClicked(object sender, EventArgs e) {
            Console.WriteLine("start");
            serialMessage.Text = "start!";
            string nowPos = "posFirst";
            Dictionary<ServoTag, double> nowD = smallWalk.posDic["posFirst"];
            Dictionary<ServoTag, double> nextD = smallWalk.posDic["pos13"];
            int frame = smallWalk.frameDic["pos13"];
            int count = 6;
            bool flag = true;
            //15msごとに指令をロボットに送る。
            while (flag) {
                Console.WriteLine(nowPos);
                //現在のポジションと次のポジションを設定
                switch (nowPos) {
                    case "posFirst":
                        nowD = smallWalk.posDic["posFirst"];
                        nextD = smallWalk.posDic["pos13"];
                        frame = smallWalk.frameDic["pos13"];
                        nowPos = "pos13";
                        break;
                    case "pos13":
                        nowD = smallWalk.posDic["pos13"];
                        nextD = smallWalk.posDic["pos45"];
                        frame = smallWalk.frameDic["pos45"];
                        nowPos = "pos45";
                        break;
                    case "pos45":
                        nowD = smallWalk.posDic["pos45"];
                        nextD = smallWalk.posDic["pos7"];
                        frame = smallWalk.frameDic["pos7"];
                        nowPos = "pos7";
                        break;
                    case "pos7":
                        nowD = smallWalk.posDic["pos7"];
                        nextD = smallWalk.posDic["pos9"];
                        frame = smallWalk.frameDic["pos9"];
                        nowPos = "pos9";
                        break;
                    case "pos9":
                        nowD = smallWalk.posDic["pos9"];
                        nextD = smallWalk.posDic["pos33"];
                        frame = smallWalk.frameDic["pos33"];
                        nowPos = "pos33";
                        break;
                    case "pos33":
                        nowD = smallWalk.posDic["pos33"];
                        --count;
                        if (count == 0) {
                            nextD = smallWalk.posDic["pos43"];
                            frame = smallWalk.frameDic["pos43"];
                            nowPos = "pos43";
                        }else {
                            nextD = smallWalk.posDic["pos0"];
                            frame = smallWalk.frameDic["pos0"];
                            nowPos = "pos0";
                        }
                        break;
                    case "pos0":
                        nowD = smallWalk.posDic["pos0"];
                        nextD = smallWalk.posDic["pos8"];
                        frame = smallWalk.frameDic["pos8"];
                        nowPos = "pos8";
                        break;
                    case "pos8":
                        nowD = smallWalk.posDic["pos8"];
                        nextD = smallWalk.posDic["pos22"];
                        frame = smallWalk.frameDic["pos22"];
                        nowPos = "pos22";
                        break;
                    case "pos22":
                        nowD = smallWalk.posDic["pos22"];
                        nextD = smallWalk.posDic["pos4"];
                        frame = smallWalk.frameDic["pos4"];
                        nowPos = "pos4";
                        break;
                    case "pos4":
                        nowD = smallWalk.posDic["pos4"];
                        --count;
                        if (count == 0) {
                            nextD = smallWalk.posDic["pos44"];
                            frame = smallWalk.frameDic["pos44"];
                            nowPos = "pos44";
                        }else {
                            nextD = smallWalk.posDic["pos1"];
                            frame = smallWalk.frameDic["pos1"];
                            nowPos = "pos1";
                        }
                        break;
                    case "pos1":
                        nowD = smallWalk.posDic["pos1"];
                        nextD = smallWalk.posDic["pos7"];
                        frame = smallWalk.frameDic["pos7"];
                        nowPos = "pos7";
                        break;
                    case "pos43":
                        nowD = smallWalk.posDic["pos43"];
                        nextD = smallWalk.posDic["posFin"];
                        frame = smallWalk.frameDic["posFin"];
                        nowPos = "posFin";
                        break;
                    case "pos44":
                        nowD = smallWalk.posDic["pos44"];
                        nextD = smallWalk.posDic["posFin"];
                        frame = smallWalk.frameDic["posFin"];
                        nowPos = "posFin";
                        break;
                    case "posFin":
                        flag = false;
                        break;
                    default:
                        break;
                }
                //コマンドを生成してロボットに送信
                for (int i = 1; i <= frame && flag; i++) {
                    servoManager.setLowerBody(ref nowD, ref nextD, (double)i / (double)frame);
                    //コマンド送信
                    System.Threading.Thread.Sleep(100);
                    serialPortManager.sendMessage(servoManager.generateCommand());
                }
                Console.WriteLine(nowPos);
                System.Threading.Thread.Sleep(10);
            }
            serialMessage.Text = "finish!";
        }
    }
}
