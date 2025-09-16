use std::fs;
use std::path::PathBuf;

use anyhow::Result;
use walkdir::WalkDir;

pub fn scan_and_collect_catalog_files_by_pattern(
    path: PathBuf,
    pattern: &str,
) -> Result<Vec<PathBuf>> {
    let mut collected_files = Vec::new();
    for entry in WalkDir::new(path.parent().unwrap())
        .into_iter()
        .filter_map(|e| e.ok())
    {
        if entry.path().is_file() && entry.path() != path {
            if entry.path().ends_with(pattern) {
                match fs::canonicalize(entry.path()) {
                    Ok(absolute_path) => {
                        collected_files.push(absolute_path);
                    }
                    Err(e) => {
                        anyhow::bail!("Cannot transform path entry to absolute path: {:#?}", e);
                    }
                }
            }
        }
    }
    Ok(collected_files)
}
