@echo off
setlocal enabledelayedexpansion

for /f "delims=" %%a in ('git rev-parse --abbrev-ref HEAD 2^>nul') do set branch=%%a

if defined branch (
    prompt=$P ($T) -!branch!- $G
) else (
    prompt=$P ($T) $G
)