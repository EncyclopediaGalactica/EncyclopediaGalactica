use clap::Command;

use self::add::add_moon_subcommand;
use self::delete::delete_moon_by_id_subcommand;
use self::get_all::get_all_moons_subcommand;
use self::update_by_id::update_moon_by_id_subcommand;

pub mod add;
pub mod delete;
pub mod get_all;
pub mod update_by_id;

pub fn galsim_bodies_moon_commands(galsim_bodies_cli: Command) -> Command {
    let moon_cli = Command::new("moons")
        .about(r#""#)
        .long_about(r#""#)
        .propagate_version(true)
        .arg_required_else_help(true)
        .color(clap::ColorChoice::Always);

    let galsim_bodies_moon_cli = add_moon_subcommand(moon_cli);
    let galsim_bodies_moon_cli = delete_moon_by_id_subcommand(galsim_bodies_moon_cli);
    let galsim_bodies_moon_cli = get_all_moons_subcommand(galsim_bodies_moon_cli);
    let galsim_bodies_moon_cli = update_moon_by_id_subcommand(galsim_bodies_moon_cli);
    galsim_bodies_cli.subcommand(galsim_bodies_moon_cli)
}
