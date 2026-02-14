pub mod cxx_qt_object;

use cxx_qt::casting::Upcast;
use cxx_qt_lib::{QGuiApplication, QQmlApplicationEngine, QQmlEngine, QUrl};
use std::pin::Pin;

fn main() {
    let mut app = QGuiApplication::new();
    let mut engine = QQmlApplicationEngine::new();

    if let Some(engine) = engine.as_mut() {
        let curdir = std::env::current_dir().unwrap();
        let qt_app_path = curdir.join("apps/qt/src/qml/main.qml");
        engine.load(&QUrl::from(qt_app_path.to_str().unwrap()));
    }

    if let Some(engine) = engine.as_mut() {
        let engine: Pin<&mut QQmlEngine> = engine.upcast_pin();
        engine.on_quit(|_| println!("Quit")).release();
    }

    if let Some(app) = app.as_mut() {
        app.exec();
    }
}
