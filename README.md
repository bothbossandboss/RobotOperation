# RobotOperation
五月祭　ロボパ　ロボット操作用
##プログラム実行方法
シリアルポートはコードにべた書きしてある。(現在はCOM5にしてある。)
プログラム実行前にデバイスマネージャでポート番号確認してコードを変更してください。

connectボタンでシリアル通信準備。
  startボタンで微小歩行を開始。

##KHR-3HV関連用語メモ
###ニュートラル
サーボモータの中心位置を表す。数値では7500。Heart To Heartでは0と表示している。

###ホームポジション
ロボットの標準の開始位置。通常は全てのモータがニュートラルになっている。

###トリム
サーボモーターで指定できる中心位置に対するずれのこと。
機体によってはホームポジションの時に立てない程、ニュートラルと希望の開始状態が離れている。
その場合にトリムを調整することで希望の開始状態を作り出せる。
この調整をトリム調整という。