##
# Ensure tabs and line endings are correct.
##

workfile="$(basename "$0" "sh").$$.tmp"

function fixup() {
	while read f; do
		gunexpand --tabs $1 --first-only <"$f" >"$workfile"
		if cmp -s "$f" "$workfile"; then
			echo "Unchanged \"$f\""
			rm -f "$workfile"
		else
			echo "Fixing \"$f\""
			gtouch --reference="$f" "$workfile"
			gchmod --reference="$f" "$workfile"
			cp -a "$f" "$f.bak"
			mv "$workfile" "$f"
		fi
	done
}

function findfiles() {
	find * -type f -name "$1" \
		! -name "$workfile" \
		! -path 'docs/*' ! -path 'coverage/*' \
		! -path '*/obj/*' ! -path '*/bin/*'
}

( findfiles '*.cs'
  findfiles '*.md'
  findfiles '*.sh'
  findfiles '*.csproj'
  findfiles '*.sln' ) \
| sort \
| fixup 4

# vi: tabstop=4 noexpandtab
