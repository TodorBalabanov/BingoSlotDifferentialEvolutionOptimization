#!/bin/bash
astyle *.cs --indent=force-tab --style=java / -A2
rm *.orig
