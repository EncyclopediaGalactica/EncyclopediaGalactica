use cli::encyclopedia_galactica_cli;

pub mod cli;

#[tokio::main]
async fn main() {
    // let env_map = dotenvy::dotenv()?;
    // println!("{:?}", env_map);
    // let root_path = "../../docs/book/";
    // let path = Path::new(root_path);
    encyclopedia_galactica_cli().await;
}
