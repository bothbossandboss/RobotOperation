/**
 * KHR-3HVの各サーボの角度を指定するクラス。
 * kinect  v2で取得した骨格の角度をサーボの角度に変換して、シリアル通信用送信データも作成する。
 * 2015/4/10時点ではkinect関連は考慮していない。
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace RobotOperation {
    /**
     * 各サーボのID
     */
    public enum ServoTag {
        NECK = 0, //首(壊れている?)
        WAIST, //腰(回転方向不明)(1)
        LEFT_SHOULDER_PITCH, //左肩ピッチ方向回転(2)
        RIGHT_SHOULDER_PITCH, //右肩ピッチ方向回転(3)
        LEFT_SHOULDER_ROLL, //左肩ロール回転(外側に開く)(4)
        RIGHT_SHOULDER_ROLL, //右肩ロール回転(内側に閉じる(鏡面対称ではない))(5)
        LEFT_SHOULDER_YAW, //左肩ヨー方向回転(6)
        RIGHT_SHOULDER_YAW, //右肩ヨー方向回転(7)
        LEFT_ELBOW, //左肘(8)
        RIGHT_ELBOW, //右肘(9)
        LEFT_HIP_YAW, //左股間節ヨー方向回転(10)
        RIGHT_HIP_YAW, //右股間節ヨー方向回転
        LEFT_HIP_ROLL, //左股関節ロール方向回転
        RIGHT_HIP_ROLL, //右股関節ロール方向回転
        LEFT_HIP_PITCH, //左股関節ピッチ方向回転
        RIGHT_HIP_PITCH, //右股関節ピッチ方向回転
        LEFT_KNEE, //左膝(16)
        RIGHT_KNEE, //右膝(17)
        LEFT_ANKLE_PITCH, //左足首ピッチ方向回転
        RIGHT_ANKLE_PITCH, //右足首ピッチ方向回転
        LEFT_ANKLE_ROLL, //左足首ロール方向回転(20)
        RIGHT_ANKLE_ROLL, //右足首ロール方向回転
        NUM_OF_SERVO
    }

    /**
     * 各サーボの角度などを格納するクラス。
     */
    public class ServoData {
        public bool changeFlag; //変更フラグ
        public bool direction; //方向の正負
        public double destAngle; //目標角度 [rad]
        public int trim; //トリム
        public int upperLimit; //上限
        public int lowerLimit; //下限
        public int offset; //オフセット

        public ServoData(bool dir, int t, int llim, int ulim) {
            changeFlag = false;
            destAngle = 0.0;
            direction = dir;
            trim = t;
            upperLimit = ulim;
            lowerLimit = llim;
            offset = 0;
        }

        public ServoData(bool dir, int t, int llim, int ulim, int ofs) {
            changeFlag = false;
            destAngle = 0.0;
            direction = dir;
            trim = t;
            upperLimit = ulim;
            lowerLimit = llim;
            offset = ofs;
        }

        public ServoData(bool dir, int t) {
            changeFlag = false;
            destAngle = 0.0;
            direction = dir;
            trim = t;
            upperLimit = 11500;
            lowerLimit = 3500;
        }

        //目標角度を設定するメソッド
        public void setDest(double d) {
            changeFlag = (d != destAngle);
            destAngle = d;
        }

        //物理的角度とサーボ制御用数値の変換
        public int toServoAngle(double angle) {
            //7500のとき0deg            
            int neutral = 7500;
            //±135deg => ±4000 
            //トリムの値も考慮する。
            int servoAngle = neutral + (int)((angle / 135.0) * 4000) + trim;
            Debug.WriteLine("servoangle : {0}", servoAngle);
            //サーボモーター最大角、最小角の設定
            //下限より小さいときは下限にする
            if (servoAngle < lowerLimit) {
                servoAngle = lowerLimit;
            }
            Debug.WriteLine("servoangle : {0}", servoAngle);
            //上限より大きいときは上限にする
            if (servoAngle > upperLimit) {
                servoAngle = upperLimit;
            }
            Debug.WriteLine("servoangle : {0}", servoAngle);

            return (int)servoAngle;
        }
    }

    /**
     * KHR-3HVのサーボを管理するクラス。
     */
    class ServoManager {
        //各サーボのIDとデータを管理する連想配列。
        private Dictionary<ServoTag, ServoData> servoDict;

        public ServoManager() {
            servoDict = new Dictionary<ServoTag, ServoData>();
            //ServoDataの第二引数はトリム値。
            servoDict.Add(ServoTag.WAIST, new ServoData(true, 0, 5500, 9500));
            servoDict.Add(ServoTag.LEFT_SHOULDER_PITCH, new ServoData(true, -1350));
            servoDict.Add(ServoTag.RIGHT_SHOULDER_PITCH, new ServoData(false, 900));
            servoDict.Add(ServoTag.LEFT_SHOULDER_ROLL, new ServoData(true, -2650, 7500, 12800, -10));
            servoDict.Add(ServoTag.RIGHT_SHOULDER_ROLL, new ServoData(false, 2650, 2200, 7500, -10));
            servoDict.Add(ServoTag.LEFT_SHOULDER_YAW, new ServoData(false, 0));
            servoDict.Add(ServoTag.RIGHT_SHOULDER_YAW, new ServoData(true, 0));
            servoDict.Add(ServoTag.LEFT_ELBOW, new ServoData(false, 2650, 3800, 8400));
            servoDict.Add(ServoTag.RIGHT_ELBOW, new ServoData(true, -2650, 6600, 11200));
            servoDict.Add(ServoTag.LEFT_KNEE, new ServoData(true, 0));
            servoDict.Add(ServoTag.RIGHT_KNEE, new ServoData(false, 0));
            servoDict.Add(ServoTag.LEFT_ANKLE_PITCH, new ServoData(true, 0));
            servoDict.Add(ServoTag.RIGHT_ANKLE_PITCH, new ServoData(false, 0));
            servoDict.Add(ServoTag.LEFT_ANKLE_ROLL, new ServoData(true, 0, 7500, 7900));
            servoDict.Add(ServoTag.RIGHT_ANKLE_ROLL, new ServoData(true, 0, 7100, 7500));
            servoDict.Add(ServoTag.LEFT_HIP_PITCH, new ServoData(false, 0));
            servoDict.Add(ServoTag.RIGHT_HIP_PITCH, new ServoData(true, 0));
            servoDict.Add(ServoTag.LEFT_HIP_ROLL, new ServoData(true, 0, 7500, 8000));//上限更新
            servoDict.Add(ServoTag.RIGHT_HIP_ROLL, new ServoData(true, 0, 7100, 8000));//上限更新
            servoDict.Add(ServoTag.NECK, new ServoData(true, 0, 6800, 8200));
        }

        public Dictionary<ServoTag, ServoData> getServo() {
            return servoDict;
        }

        /**
         * Heart to Heart のサンプルモーションの微小歩行の数値をべた書き。
         * フレーム周期は15msとする。(ロボットの接続が数値確認に必要だったので正しいとは限らない。)
         * 前のポジションから設定したポジションへの移動には、フレーム数*フレーム周期の時間がかかる。
         */
        //Heart to Heart の数値を物理的角度に変換して入力する。
        //フレーム数20
        //ニュートラル -> Pos13
        public void setPos13() {
            double transN = 135.0 / 4000.0;
                servoDict[ServoTag.LEFT_HIP_ROLL].setDest(150.0 * transN);//(12)
                servoDict[ServoTag.RIGHT_HIP_ROLL].setDest(150.0 * transN);//(13)
                servoDict[ServoTag.LEFT_HIP_PITCH].setDest(500.0 * transN);//(14)
                servoDict[ServoTag.RIGHT_HIP_PITCH].setDest(-500.0 * transN);//(15)
                servoDict[ServoTag.LEFT_KNEE].setDest(1000.0 * transN);//(16)
                servoDict[ServoTag.RIGHT_KNEE].setDest(-1000.0 * transN);//(17)
                servoDict[ServoTag.LEFT_ANKLE_PITCH].setDest(-650.0 * transN);//(18)
                servoDict[ServoTag.RIGHT_ANKLE_PITCH].setDest(650.0 * transN);//(19)
                servoDict[ServoTag.LEFT_ANKLE_ROLL].setDest(150.0 * transN);//(20)
                servoDict[ServoTag.RIGHT_ANKLE_ROLL].setDest(150.0 * transN);//(21)
        }

        //フレーム数12
        public void setPos45() {
            double transN = 135.0 / 4000.0;
            servoDict[ServoTag.LEFT_HIP_ROLL].setDest(400.0 * transN);
            servoDict[ServoTag.RIGHT_HIP_ROLL].setDest(250.0 * transN);
            servoDict[ServoTag.LEFT_HIP_PITCH].setDest(1100.0 * transN);
            servoDict[ServoTag.RIGHT_HIP_PITCH].setDest(-600.0 * transN);
            servoDict[ServoTag.LEFT_KNEE].setDest(2200.0 * transN);
            servoDict[ServoTag.RIGHT_KNEE].setDest(-1200.0 * transN);
            servoDict[ServoTag.LEFT_ANKLE_PITCH].setDest(-1100.0 * transN);
            servoDict[ServoTag.RIGHT_ANKLE_PITCH].setDest(750.0 * transN);
            servoDict[ServoTag.LEFT_ANKLE_ROLL].setDest(450.0 * transN);
            servoDict[ServoTag.RIGHT_ANKLE_ROLL].setDest(250.0 * transN);
        }

        //フレーム数17
        public void setPos7() {
            double transN = 135.0 / 4000.0;
            servoDict[ServoTag.LEFT_HIP_ROLL].setDest(0 * transN);
            servoDict[ServoTag.RIGHT_HIP_ROLL].setDest(0 * transN);
            servoDict[ServoTag.LEFT_HIP_PITCH].setDest(900.0 * transN);
            servoDict[ServoTag.RIGHT_HIP_PITCH].setDest(-500.0 * transN);
            servoDict[ServoTag.LEFT_KNEE].setDest(1200.0 * transN);
            servoDict[ServoTag.RIGHT_KNEE].setDest(-1200.0 * transN);
            servoDict[ServoTag.LEFT_ANKLE_PITCH].setDest(-450.0 * transN);
            servoDict[ServoTag.RIGHT_ANKLE_PITCH].setDest(900.0 * transN);
            servoDict[ServoTag.LEFT_ANKLE_ROLL].setDest(0 * transN);
            servoDict[ServoTag.RIGHT_ANKLE_ROLL].setDest(-100.0 * transN);
        }

        //フレーム数
        public void setPos9() {
            double transN = 135.0 / 4000.0;
            servoDict[ServoTag.LEFT_HIP_ROLL].setDest(-100 * transN);
            servoDict[ServoTag.RIGHT_HIP_ROLL].setDest(-50 * transN);
            servoDict[ServoTag.LEFT_HIP_PITCH].setDest(500.0 * transN);
            servoDict[ServoTag.RIGHT_HIP_PITCH].setDest(250.0 * transN);
            servoDict[ServoTag.LEFT_KNEE].setDest(1200.0 * transN);
            servoDict[ServoTag.RIGHT_KNEE].setDest(-700.0 * transN);
            servoDict[ServoTag.LEFT_ANKLE_PITCH].setDest(-800.0 * transN);
            servoDict[ServoTag.RIGHT_ANKLE_PITCH].setDest(900.0 * transN);
            servoDict[ServoTag.LEFT_ANKLE_ROLL].setDest(-200 * transN);
            servoDict[ServoTag.RIGHT_ANKLE_ROLL].setDest(-200.0 * transN);
        }

        //フレーム数20
        public void setPos33() {
            double transN = 135.0 / 4000.0;
            servoDict[ServoTag.LEFT_HIP_ROLL].setDest(-250 * transN);
            servoDict[ServoTag.RIGHT_HIP_ROLL].setDest(-250 * transN);
            servoDict[ServoTag.LEFT_HIP_PITCH].setDest(600.0 * transN);
            servoDict[ServoTag.RIGHT_HIP_PITCH].setDest(-300.0 * transN);
            servoDict[ServoTag.LEFT_KNEE].setDest(1200.0 * transN);
            servoDict[ServoTag.RIGHT_KNEE].setDest(-1700.0 * transN);
            servoDict[ServoTag.LEFT_ANKLE_PITCH].setDest(-700.0 * transN);
            servoDict[ServoTag.RIGHT_ANKLE_PITCH].setDest(1000.0 * transN);
            servoDict[ServoTag.LEFT_ANKLE_ROLL].setDest(-300 * transN);
            servoDict[ServoTag.RIGHT_ANKLE_ROLL].setDest(-300.0 * transN);
        }

        //フレーム数12
        public void setPos0() {
            double transN = 135.0 / 4000.0;
            servoDict[ServoTag.LEFT_HIP_ROLL].setDest(-250 * transN);
            servoDict[ServoTag.RIGHT_HIP_ROLL].setDest(-400 * transN);
            servoDict[ServoTag.LEFT_HIP_PITCH].setDest(600.0 * transN);
            servoDict[ServoTag.RIGHT_HIP_PITCH].setDest(-1100.0 * transN);
            servoDict[ServoTag.LEFT_KNEE].setDest(1200.0 * transN);
            servoDict[ServoTag.RIGHT_KNEE].setDest(-2200.0 * transN);
            servoDict[ServoTag.LEFT_ANKLE_PITCH].setDest(-700.0 * transN);
            servoDict[ServoTag.RIGHT_ANKLE_PITCH].setDest(1100.0 * transN);
            servoDict[ServoTag.LEFT_ANKLE_ROLL].setDest(-250 * transN);
            servoDict[ServoTag.RIGHT_ANKLE_ROLL].setDest(-450.0 * transN);
        }

        //フレーム数17
        public void setPos8() {
            double transN = 135.0 / 4000.0;
            servoDict[ServoTag.LEFT_HIP_ROLL].setDest(0 * transN);
            servoDict[ServoTag.RIGHT_HIP_ROLL].setDest(0 * transN);
            servoDict[ServoTag.LEFT_HIP_PITCH].setDest(500.0 * transN);
            servoDict[ServoTag.RIGHT_HIP_PITCH].setDest(-900.0 * transN);
            servoDict[ServoTag.LEFT_KNEE].setDest(1200.0 * transN);
            servoDict[ServoTag.RIGHT_KNEE].setDest(-1200.0 * transN);
            servoDict[ServoTag.LEFT_ANKLE_PITCH].setDest(-900.0 * transN);
            servoDict[ServoTag.RIGHT_ANKLE_PITCH].setDest(450.0 * transN);
            servoDict[ServoTag.LEFT_ANKLE_ROLL].setDest(100 * transN);
            servoDict[ServoTag.RIGHT_ANKLE_ROLL].setDest(0 * transN);
        }

        //フレーム数20
        public void setPos22() {
            double transN = 135.0 / 4000.0;
            servoDict[ServoTag.LEFT_HIP_ROLL].setDest(50 * transN);
            servoDict[ServoTag.RIGHT_HIP_ROLL].setDest(100 * transN);
            servoDict[ServoTag.LEFT_HIP_PITCH].setDest(-250.0 * transN);
            servoDict[ServoTag.RIGHT_HIP_PITCH].setDest(-500.0 * transN);
            servoDict[ServoTag.LEFT_KNEE].setDest(700.0 * transN);
            servoDict[ServoTag.RIGHT_KNEE].setDest(-1200.0 * transN);
            servoDict[ServoTag.LEFT_ANKLE_PITCH].setDest(-900.0 * transN);
            servoDict[ServoTag.RIGHT_ANKLE_PITCH].setDest(800.0 * transN);
            servoDict[ServoTag.LEFT_ANKLE_ROLL].setDest(200 * transN);
            servoDict[ServoTag.RIGHT_ANKLE_ROLL].setDest(200 * transN);
        }

        //フレーム数20
        public void setPos4() {
            double transN = 135.0 / 4000.0;
            servoDict[ServoTag.LEFT_HIP_ROLL].setDest(250 * transN);
            servoDict[ServoTag.RIGHT_HIP_ROLL].setDest(250 * transN);
            servoDict[ServoTag.LEFT_HIP_PITCH].setDest(300.0 * transN);
            servoDict[ServoTag.RIGHT_HIP_PITCH].setDest(-600.0 * transN);
            servoDict[ServoTag.LEFT_KNEE].setDest(1700.0 * transN);
            servoDict[ServoTag.RIGHT_KNEE].setDest(-1200.0 * transN);
            servoDict[ServoTag.LEFT_ANKLE_PITCH].setDest(-1000.0 * transN);
            servoDict[ServoTag.RIGHT_ANKLE_PITCH].setDest(700.0 * transN);
            servoDict[ServoTag.LEFT_ANKLE_ROLL].setDest(300 * transN);
            servoDict[ServoTag.RIGHT_ANKLE_ROLL].setDest(300 * transN);
        }

        //フレーム数12
        public void setPos1() {
            double transN = 135.0 / 4000.0;
            servoDict[ServoTag.LEFT_HIP_ROLL].setDest(400 * transN);
            servoDict[ServoTag.RIGHT_HIP_ROLL].setDest(250 * transN);
            servoDict[ServoTag.LEFT_HIP_PITCH].setDest(1100.0 * transN);
            servoDict[ServoTag.RIGHT_HIP_PITCH].setDest(-600.0 * transN);
            servoDict[ServoTag.LEFT_KNEE].setDest(2200.0 * transN);
            servoDict[ServoTag.RIGHT_KNEE].setDest(-1200.0 * transN);
            servoDict[ServoTag.LEFT_ANKLE_PITCH].setDest(-1100.0 * transN);
            servoDict[ServoTag.RIGHT_ANKLE_PITCH].setDest(700.0 * transN);
            servoDict[ServoTag.LEFT_ANKLE_ROLL].setDest(450 * transN);
            servoDict[ServoTag.RIGHT_ANKLE_ROLL].setDest(250 * transN);
        }

        //フレーム数15
        public void setPos43() {
            double transN = 135.0 / 4000.0;
            servoDict[ServoTag.LEFT_HIP_ROLL].setDest(-250 * transN);
            servoDict[ServoTag.RIGHT_HIP_ROLL].setDest(-250 * transN);
            servoDict[ServoTag.LEFT_HIP_PITCH].setDest(600.0 * transN);
            servoDict[ServoTag.RIGHT_HIP_PITCH].setDest(-900.0 * transN);
            servoDict[ServoTag.LEFT_KNEE].setDest(1200.0 * transN);
            servoDict[ServoTag.RIGHT_KNEE].setDest(-2100.0 * transN);
            servoDict[ServoTag.LEFT_ANKLE_PITCH].setDest(-700.0 * transN);
            servoDict[ServoTag.RIGHT_ANKLE_PITCH].setDest(1300.0 * transN);
            servoDict[ServoTag.LEFT_ANKLE_ROLL].setDest(-300 * transN);
            servoDict[ServoTag.RIGHT_ANKLE_ROLL].setDest(-300 * transN);
        }

        //フレーム数15
        public void setPos44() {
            double transN = 135.0 / 4000.0;
            servoDict[ServoTag.LEFT_HIP_ROLL].setDest(250 * transN);
            servoDict[ServoTag.RIGHT_HIP_ROLL].setDest(250 * transN);
            servoDict[ServoTag.LEFT_HIP_PITCH].setDest(900.0 * transN);
            servoDict[ServoTag.RIGHT_HIP_PITCH].setDest(-600.0 * transN);
            servoDict[ServoTag.LEFT_KNEE].setDest(2100.0 * transN);
            servoDict[ServoTag.RIGHT_KNEE].setDest(-1200.0 * transN);
            servoDict[ServoTag.LEFT_ANKLE_PITCH].setDest(-1300.0 * transN);
            servoDict[ServoTag.RIGHT_ANKLE_PITCH].setDest(700.0 * transN);
            servoDict[ServoTag.LEFT_ANKLE_ROLL].setDest(300 * transN);
            servoDict[ServoTag.RIGHT_ANKLE_ROLL].setDest(300 * transN);
        }

        //フレーム数30
        public void setPosFin() {
            double transN = 135.0 / 4000.0;
            servoDict[ServoTag.LEFT_HIP_ROLL].setDest(0 * transN);
            servoDict[ServoTag.RIGHT_HIP_ROLL].setDest(0 * transN);
            servoDict[ServoTag.LEFT_HIP_PITCH].setDest(500.0 * transN);
            servoDict[ServoTag.RIGHT_HIP_PITCH].setDest(-500.0 * transN);
            servoDict[ServoTag.LEFT_KNEE].setDest(1000.0 * transN);
            servoDict[ServoTag.RIGHT_KNEE].setDest(-1000.0 * transN);
            servoDict[ServoTag.LEFT_ANKLE_PITCH].setDest(-600.0 * transN);
            servoDict[ServoTag.RIGHT_ANKLE_PITCH].setDest(600.0 * transN);
            servoDict[ServoTag.LEFT_ANKLE_ROLL].setDest(0 * transN);
            servoDict[ServoTag.RIGHT_ANKLE_ROLL].setDest(0 * transN);
        }

        public void setLowerBody(ref Dictionary<ServoTag, double> prePos, ref Dictionary<ServoTag, double> nextPos, double now) {
            double transN = 135.0 / 4000.0;
            double theta = (prePos[ServoTag.LEFT_HIP_ROLL] + (nextPos[ServoTag.LEFT_HIP_ROLL] - prePos[ServoTag.LEFT_HIP_ROLL]) * now) * transN;
            servoDict[ServoTag.LEFT_HIP_ROLL].setDest(theta);
            
            theta = (prePos[ServoTag.RIGHT_HIP_ROLL] + (nextPos[ServoTag.RIGHT_HIP_ROLL] -prePos[ServoTag.RIGHT_HIP_ROLL]) * now) * transN;
            servoDict[ServoTag.RIGHT_HIP_ROLL].setDest(theta);
            
            theta = (prePos[ServoTag.LEFT_HIP_PITCH] +( nextPos[ServoTag.LEFT_HIP_PITCH] - prePos[ServoTag.LEFT_HIP_PITCH] ) * now) * transN;
            servoDict[ServoTag.LEFT_HIP_PITCH].setDest(theta);
            
            theta = (prePos[ServoTag.RIGHT_HIP_PITCH] + (nextPos[ServoTag.RIGHT_HIP_PITCH] -  prePos[ServoTag.RIGHT_HIP_PITCH]) * now)  * transN;
            servoDict[ServoTag.RIGHT_HIP_PITCH].setDest(theta);
            
            theta = (prePos[ServoTag.LEFT_KNEE] + (nextPos[ServoTag.LEFT_KNEE] - prePos[ServoTag.LEFT_KNEE]) * now) * transN;
            servoDict[ServoTag.LEFT_KNEE].setDest(theta);
            
            theta = (prePos[ServoTag.RIGHT_KNEE] + (nextPos[ServoTag.RIGHT_KNEE] - prePos[ServoTag.RIGHT_KNEE]) * now) * transN;
            servoDict[ServoTag.RIGHT_KNEE].setDest(theta);
            
            theta = (prePos[ServoTag.LEFT_ANKLE_PITCH] + (nextPos[ServoTag.LEFT_ANKLE_PITCH] - prePos[ServoTag.LEFT_ANKLE_PITCH]) * now) * transN;
            servoDict[ServoTag.LEFT_ANKLE_PITCH].setDest(theta);
            
            theta = (prePos[ServoTag.RIGHT_ANKLE_PITCH] + (nextPos[ServoTag.RIGHT_ANKLE_PITCH] - prePos[ServoTag.RIGHT_ANKLE_PITCH]) * now) * transN;
            servoDict[ServoTag.RIGHT_ANKLE_PITCH].setDest(theta);
            
            theta = (prePos[ServoTag.LEFT_ANKLE_ROLL] + (nextPos[ServoTag.LEFT_ANKLE_ROLL] - prePos[ServoTag.LEFT_ANKLE_ROLL]) * now) * transN;
            servoDict[ServoTag.LEFT_ANKLE_ROLL].setDest(theta);
            
            theta = (prePos[ServoTag.RIGHT_ANKLE_ROLL] + (nextPos[ServoTag.RIGHT_ANKLE_ROLL] - prePos[ServoTag.RIGHT_ANKLE_ROLL]) * now) * transN;
            servoDict[ServoTag.RIGHT_ANKLE_ROLL].setDest(theta);
        }

        /**
         * 城さんのプログラムでは、kinect v2から骨格の角度を割り出し、 
         * public void calcServoDest(Dictionary<BoneTag, Vector3D> boneDict)
         * でservoDictの値を更新していた。
         * 2015/4/10時点ではservoDictを更新するメソッドは追加していない。
         */

        //物理的角度表記からサーボ角度表記へ変換
        private Dictionary<int, int> servoToDest(Dictionary<ServoTag, ServoData> d) {
            Dictionary<int, int> ret = new Dictionary<int, int>(d.Count);
            foreach (ServoTag s in servoDict.Keys) {
                if (d[s].changeFlag) {
                    Debug.WriteLine("servo ID {0}", (int)s);
//                    if (d[s].direction) {
                        ret.Add((int)s, d[s].toServoAngle(d[s].destAngle));
//                   } else {
    //                    ret.Add((int)s, d[s].toServoAngle(-d[s].destAngle));
       //             }
                }
            }
            return ret;
        }

        //シリアル通信で送るべき、各サーボの角度指令を連想配列から生成。
        public byte[] generateCommand() {
            byte[] cmd = CommandUtil.SeriesServoMove(servoToDest(servoDict), 25);
            return cmd;
        }
    }
}
