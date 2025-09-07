use std::path::Path;

use clap::ArgMatches;

// use crate::logic::arg_collectors;
// use crate::logic::controllers;

pub async fn generate_matchers(arg_matches: ArgMatches, base_path: &Path) {
    match arg_matches.subcommand() {
        Some(("generate", generate_subcommand_matches)) => {
            match generate_subcommand_matches.subcommand_matches("book") {
                Some(book_match) => {
                    // let args = arg_collectors::generate_from_a_book::generate_from_a_book(
                    //     book_match.clone(),
                    // )
                    // .unwrap_or_else(|e| panic!("Failed parsing the arguments. Error: {}", e));
                    // // logic::generate_from_a_book::execute(args, base_path)
                    // println!("book generation");
                    // match controllers::generate_from_a_book::execute(base_path, args).await {
                    //     Ok(_) => {
                    //         println!("Book was generated successfully.");
                    //     }
                    //     Err(_) => {
                    //         println!("Book generation failed.");
                    //     }
                    // }
                }
                None => {}
            }
            // match generate_subcommand_matches.subcommand_matches("topic") {
            //     Some(topic_matches) => {
            //         println!("generate book matches: {:#?}", topic_matches);
            //         panic!("whatever")
            //     }
            //     None => {}
            // }
        }
        _ => {}
    };
}
