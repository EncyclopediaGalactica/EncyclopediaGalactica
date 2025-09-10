use std::collections::HashMap;
use std::fs;
use std::path::Path;
use std::path::PathBuf;

use anyhow::Result;
use clap::Error;
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

pub fn scan_and_collect_catalog_files(path: &Path) -> Result<HashMap<String, Vec<PathBuf>>, Error> {
    let mut files: HashMap<String, Vec<PathBuf>> = HashMap::new();
    files.insert("topic".to_string(), Vec::new());
    files.insert("book".to_string(), Vec::new());
    files.insert("chapter".to_string(), Vec::new());
    files.insert("section".to_string(), Vec::new());

    for entry in WalkDir::new(path.parent().unwrap())
        .into_iter()
        .filter_map(|e| e.ok())
    {
        if entry.path().is_file() && entry.path() != Path::new(path) {
            if entry.path().ends_with("topic.toml") {
                match fs::canonicalize(entry.path()) {
                    Ok(absolute_path) => {
                        files
                            .get_mut("topic")
                            .map(|topic_list| topic_list.push(absolute_path));
                    }
                    Err(e) => {
                        panic!("Cannot transform path entry to absolute path: {:#?}", e);
                    }
                }
            }
            // if entry.path().ends_with("book.toml") {
            //     match fs::canonicalize(entry.path()) {
            //         Ok(absolute_path) => {
            //             files
            //                 .get_mut("book")
            //                 .map(|topic_list| topic_list.push(absolute_path));
            //         }
            //         Err(e) => {
            //             panic!("Cannot transform path entry to absolute path: {:#?}", e);
            //         }
            //     }
            // }
            // if entry.path().ends_with("chapter.toml") {
            //     match fs::canonicalize(entry.path()) {
            //         Ok(absolute_path) => {
            //             files
            //                 .get_mut("chapter")
            //                 .map(|topic_list| topic_list.push(absolute_path));
            //         }
            //         Err(e) => {
            //             panic!("Cannot transform path entry to absolute path: {:#?}", e);
            //         }
            //     }
            // }
            // if entry.path().ends_with("section.toml") {
            //     match fs::canonicalize(entry.path()) {
            //         Ok(absolute_path) => {
            //             files
            //                 .get_mut("section")
            //                 .map(|topic_list| topic_list.push(absolute_path));
            //         }
            //         Err(e) => {
            //             panic!("Cannot transform path entry to absolute path: {:#?}", e);
            //         }
            //     }
            // }
        }
    }
    Ok(files)
}
