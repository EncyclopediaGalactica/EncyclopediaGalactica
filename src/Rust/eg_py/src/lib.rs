use once_cell::sync::Lazy;
use pyo3::Bound;
use pyo3::PyResult;
use pyo3::pyfunction;
use pyo3::pymodule;
use pyo3::types::PyModule;
use pyo3::types::PyModuleMethods;
use pyo3::wrap_pyfunction;
use starmap::scenarios::planets::add::add::add_planet_scenario;
use starmap::scenarios::planets::add::types::AddPlanetScenarioInput;
use starmap::scenarios::planets::add::types::AddPlanetScenarioResult;
use tokio::runtime::Runtime;

static RT: Lazy<Runtime> = Lazy::new(|| Runtime::new().expect("Failed to create Tokio runtime"));

#[pyfunction]
fn add_planet(input: AddPlanetScenarioInput) -> PyResult<AddPlanetScenarioResult> {
    let _db_connection_string = "";
    let result = RT.block_on(async { add_planet_scenario(input, None, None).await })?;
    Ok(result)
}

#[pymodule]
fn eg_python_api(module: &Bound<'_, PyModule>) -> PyResult<()> {
    // starmap submodule
    let starmap_submodule = PyModule::new(module.py(), "starmap")?;
    starmap_submodule.add_function(wrap_pyfunction!(add_planet, &starmap_submodule)?)?;
    module.add_submodule(&starmap_submodule)?;
    Ok(())
}
