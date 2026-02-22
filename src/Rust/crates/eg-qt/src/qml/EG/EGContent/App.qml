import QtQuick
import QtQuick.Controls
import EG

ApplicationWindow {
    id: applicationWindow
    width: 720
    height: 520
    visible: true
    title: "Encyclopedia Galactica"

    menuBar: MenuBar {
        Menu {
            title: qsTr("&File")
            Action { text: qsTr("&New...") }
            Action { text: qsTr("&Open...") }
            Action { text: qsTr("&Save") }
            Action { text: qsTr("Save &As...") }
            MenuSeparator { }
            Action { text: qsTr("&Quit") }
        }
        Menu {
            title: qsTr("&Edit")
            Action { text: qsTr("Cu&t") }
            Action { text: qsTr("&Copy") }
            Action { text: qsTr("&Paste") }
        }
        Menu {
            title: "Modules"
            Action { text: qsTr("Galaxy Simulator") }
        }
        Menu {
            title: qsTr("&Help")
            Action { text: qsTr("&About") }
        }
    }

    footer: Rectangle {
        width: parent.width
        height: parent.height
    }

    TabBar {
        id: tabBar
        width: parent.width
        TabButton {
            text: qsTr("First")
        }
        TabButton {
            text: qsTr("Second")
        }

    }

}
