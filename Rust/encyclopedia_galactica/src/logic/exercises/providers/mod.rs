use chrono::DateTime;
use chrono::Local;

pub fn provide_filename() -> anyhow::Result<String> {
    let now: DateTime<Local> = Local::now();
    Ok(now.format("%Y-%m-%d_%H-%M-%S").to_string())
}
