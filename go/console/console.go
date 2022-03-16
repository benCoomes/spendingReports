package main

import (
	"fmt"
	"os"
	"path/filepath"
	"strings"
)

func main() {
	filename := "../testTransactions.csv"

	transactions := ReadTransactionsFromFile(filename)

	for _, transaction := range transactions {
		fmt.Println(transaction)
	}
}

type Transaction struct {
	date    string
	amount  float32
	details string
}

func (t Transaction) String() string {
	return fmt.Sprintf("[%v] [%v] [%v]", t.date, t.amount, t.details)
}

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func ReadTransactionsFromFile(path string) []Transaction {
	absPath, err := filepath.Abs(path)
	check(err)

	contents, err := os.ReadFile(absPath)
	check(err)

	lines := strings.Split(string(contents), "\n")
	transactions := make([]Transaction, len(lines))

	for i, line := range lines {
		transactions[i] = Transaction{
			date:    "2021-12-01",
			amount:  10.50,
			details: line,
		}
	}

	return transactions
}
