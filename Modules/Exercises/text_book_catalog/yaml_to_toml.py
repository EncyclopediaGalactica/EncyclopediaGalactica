import yaml
import toml
import sys
import os


def convert_yaml_to_toml(yaml_file_path, toml_file_path):
    try:
        # Read YAML file
        with open(yaml_file_path, "r") as yaml_file:
            yaml_data = yaml.safe_load(yaml_file)

        # Convert to TOML and write to file
        with open(toml_file_path, "w") as toml_file:
            toml.dump(yaml_data, toml_file)

        print(f"Successfully converted {yaml_file_path} to {toml_file_path}")

    except FileNotFoundError:
        print(f"Error: Input file {yaml_file_path} not found")
    except yaml.YAMLError as e:
        print(f"Error parsing YAML in {yaml_file_path}: {e}")
    except Exception as e:
        print(f"Unexpected error converting {yaml_file_path}: {e}")


def process_directory(input_dir):
    yaml_files_found = 0
    for root, dirs, files in os.walk(input_dir):
        for file in files:
            if file.lower().endswith((".yaml", ".yml")):
                yaml_path = os.path.join(root, file)
                toml_path = os.path.splitext(yaml_path)[0] + ".toml"
                convert_yaml_to_toml(yaml_path, toml_path)
                yaml_files_found += 1
    if yaml_files_found == 0:
        print(f"No YAML files found in directory {input_dir} or its subdirectories.")


def main():
    if len(sys.argv) < 2:
        print("Usage:")
        print(
            "  For single file: python yaml_to_toml_extended.py input.yaml output.toml"
        )
        print("  For directory: python yaml_to_toml_extended.py /path/to/directory")
        sys.exit(1)

    input_path = sys.argv[1]

    if os.path.isdir(input_path):
        # Directory mode: recursive conversion
        process_directory(input_path)
    elif os.path.isfile(input_path):
        # Single file mode
        if len(sys.argv) != 3:
            print(
                "Error: For single file conversion, provide both input.yaml and output.toml"
            )
            sys.exit(1)
        output_path = sys.argv[2]
        convert_yaml_to_toml(input_path, output_path)
    else:
        print(f"Error: {input_path} is not a valid file or directory")
        sys.exit(1)


if __name__ == "__main__":
    main()
