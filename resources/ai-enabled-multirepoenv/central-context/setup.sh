#!/usr/bin/env bash
#
# Asentaa central-context/root-ai-files -sisällön (AGENTS.md, skills/) aidrivenworkflow-kansion
# juureen symlinkkeinä, ja kloonaa (valinnaisesti) tiimin repot workflow-juureen.
#
# Käyttö:
#   cd aidrivenworkflow/central-context
#   ./setup.sh              # vain symlinkit
#   ./setup.sh --clone       # symlinkit + repos.json:in kloonaus
#   ./setup.sh --force       # ylikirjoita olemassaolevat symlinkit/kopiot
#
set -euo pipefail

CONTEXT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
WORKFLOW_ROOT="$(dirname "$CONTEXT_DIR")"
ROOT_AI_FILES="$CONTEXT_DIR/root-ai-files"

CLONE_REPOS=0
FORCE=0
for arg in "$@"; do
    case "$arg" in
        --clone) CLONE_REPOS=1 ;;
        --force) FORCE=1 ;;
        *) echo "Tuntematon parametri: $arg" >&2; exit 1 ;;
    esac
done

link_or_copy() {
    local source_path="$1"
    local target_path="$2"

    if [ -e "$target_path" ] || [ -L "$target_path" ]; then
        if [ "$FORCE" -ne 1 ]; then
            echo "Skip: '$target_path' on jo olemassa. Käytä --force ylikirjoittaaksesi." >&2
            return
        fi
        rm -rf "$target_path"
    fi

    if ln -s "$source_path" "$target_path" 2>/dev/null; then
        echo "OK  symlink: $target_path -> $source_path"
    else
        echo "Symlink epäonnistui, kopioidaan sen sijaan (ei pysy synkassa muutoksille)." >&2
        cp -r "$source_path" "$target_path"
        echo "OK  kopio: $target_path (lähde: $source_path)"
    fi
}

echo "== AI-driven multirepo setup =="
echo "Workflow root: $WORKFLOW_ROOT"

# 1) AGENTS.md juureen
link_or_copy "$ROOT_AI_FILES/AGENTS.md" "$WORKFLOW_ROOT/AGENTS.md"

# 2) yhteiset skillsit juureen
link_or_copy "$ROOT_AI_FILES/skills" "$WORKFLOW_ROOT/skills"

# 3) valinnainen: kloonaa repos.json:issa listatut repot workflow-juureen
if [ "$CLONE_REPOS" -eq 1 ]; then
    REPOS_FILE="$CONTEXT_DIR/repos.json"
    if [ ! -f "$REPOS_FILE" ]; then
        echo "repos.json puuttuu ($REPOS_FILE), ohitetaan kloonaus." >&2
    else
        # vaatii jq:n JSON-parsintaan
        count=$(jq 'length' "$REPOS_FILE")
        for ((i = 0; i < count; i++)); do
            name=$(jq -r ".[$i].name" "$REPOS_FILE")
            url=$(jq -r ".[$i].url" "$REPOS_FILE")
            dest="$WORKFLOW_ROOT/$name"
            if [ -d "$dest" ]; then
                echo "Skip clone, kansio olemassa: $name"
                continue
            fi
            echo "Kloonataan $name <- $url"
            git clone "$url" "$dest"
        done
    fi
fi

echo "Valmis."
