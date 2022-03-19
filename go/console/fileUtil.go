package main

import (
	"errors"
	"fmt"
	"os"
	"path/filepath"
	"regexp"
	"strconv"
	"strings"
)

func ParseDate(dateString string) (string, error) {
	return strings.TrimSpace(dateString), nil
}

func ParseFloat64(floatString string) (float64, error) {
	floatString = strings.TrimSpace(floatString)

	amount, err := strconv.ParseFloat(floatString, 64)
	if err != nil {
		errtext := fmt.Sprintf("Failed to parse amount into a number. String value: %v", floatString)
		return 0, errors.New(errtext)
	}

	return float64(amount), nil
}

func ParseString(input string) (string, error) {
	return strings.TrimSpace(input), nil
}

func ParseStringSlice(input string, sep string) []string {
	pieces := strings.Split(input, sep)
	for i, piece := range pieces {
		pieces[i] = strings.TrimSpace(piece)
	}
	return pieces
}

func ParseRegex(regexString string) (*regexp.Regexp, error) {
	regexString = strings.TrimSpace(regexString)

	regex, err := regexp.Compile(regexString)
	if err != nil {
		errtext := fmt.Sprintf("Failed to parse into a regular expression. String value: %v", regexString)
		return nil, errors.New(errtext)
	}

	return regex, nil
}

func ReadAsString(path string) string {
	absPath, err := filepath.Abs(path)
	check(err)

	contents, err := os.ReadFile(absPath)
	check(err)

	return string(contents)
}
