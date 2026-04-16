rom pathlib import Path

folder_path = Path(".")
txt_files = list(folder_path.glob("*.txt"))
print(txt_files)


unique_imos = set()
for file in txt_files:
    for line in file.read_text().splitlines():
        line = line.strip()
        if line:
            unique_imos.add(line)


Path("combined.txt").write_text("\n".join(sorted(unique_imos)))