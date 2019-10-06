package com.company;

import java.util.ArrayList;
import java.util.Scanner;

public class Main {

    private static ParseEngine parser = new ParseEngine();

    public static void main(String[] args) {
        Scanner userInput = new Scanner(System.in);
        String example = userInput.nextLine();

        parser.parseString(example);
        System.out.println(parser.getResult());
    }
}

class ParseEngine{

    enum Operands{Plus, Minus, Multiply, Division};

    private ArrayList<String> numbers = new ArrayList<>();
    private ArrayList<Operands> operands = new ArrayList<>();
    private ArrayList<Integer> priority = new ArrayList<>();

    private void addOperand(char operand){
        switch (operand){
            case '+':
                operands.add(Operands.Plus);
                break;
            case '-':
                operands.add(Operands.Minus);
                break;
            case '*':
                operands.add(Operands.Multiply);
                break;
            case '/':
                operands.add(Operands.Division);
                break;
        }
    }

    private boolean isOperand(char symbol){
        return (symbol == '+' || symbol == '-' || symbol == '*' || symbol == '/');
    }

    private void resetExample(){
        numbers.clear();
        operands.clear();
        priority.clear();
    }

    public void parseString(String example){
        resetExample();
        StringBuilder temp = new StringBuilder();

        int priority = 0;
        int openBrackets = 0;
        int closeBrackets = 0;

        for(int i = 0; i < example.length(); i++){

            if (example.charAt(i) == '('){
                openBrackets++;
                priority++;
            }
            else if(example.charAt(i) == ')'){
                closeBrackets++;
                priority--;
            }

            if (!isOperand(example.charAt(i)) && example.charAt(i) != '(' && example.charAt(i) != ')'){
                temp.append(example.charAt(i));
            }
            else if(isOperand(example.charAt(i))){
                numbers.add(String.valueOf(temp));
                temp.setLength(0);
                addOperand(example.charAt(i));
                this.priority.add(priority);
            }

            if(i == example.length() - 1 && temp.length() > 0){
                numbers.add(String.valueOf(temp));
                temp.setLength(0);
            }
        }

        if(checkCorrectness(openBrackets, closeBrackets)) eval();
    }

    //возвращает индекс операнда с наибольшим приоритетом
    private int getHighestPriorityIndex(Operands operandFirst, Operands operandsSecond){
        int priorityIndex = getHighestPriority();
        for(int i = 0; i < priority.size(); i++){
            if(priority.get(i) == priorityIndex &&
                   (operands.get(i) == operandFirst || operands.get(i) == operandsSecond)){
                return i;
            }
        }
        return -1;
    }

    //возвращает наибольший приоритет
    private int getHighestPriority(){
        int highestPriority = 0;
        for(Integer priority: this.priority){
            if(priority > highestPriority) highestPriority = priority;
        }
        return highestPriority;
    }

    private void editNumbersOperands(int index, double result){
        numbers.set(index, String.valueOf(result));
        numbers.remove(index + 1);
        operands.remove(index);
        priority.remove(index);
    }

    private boolean checkCorrectness(int countOpenBrackets, int countCloseBrackets){
        if (numbers.size() == operands.size() ||
                countOpenBrackets - countCloseBrackets != 0){
            resetExample();
            numbers.add("");
            return false;
        }
        return true;
    }

    private void eval(){
        while(operands.size() > 0){

            int i = getHighestPriorityIndex(Operands.Multiply, Operands.Division);
            double tempResult;
            while(i != -1){
                i = getHighestPriorityIndex(Operands.Multiply, Operands.Division);
                if(i == -1){
                    break;
                }
                if(operands.get(i) == Operands.Multiply){
                    tempResult = Double.parseDouble(numbers.get(i)) * Double.parseDouble(numbers.get(i+1));
                    editNumbersOperands(i, tempResult);
                }
                else if(operands.get(i) == Operands.Division){
                    tempResult = Double.parseDouble(numbers.get(i)) / Double.parseDouble(numbers.get(i+1));
                    editNumbersOperands(i, tempResult);
                }
            }

            i = getHighestPriorityIndex(Operands.Plus, Operands.Minus);
            while(i != -1){
                i = getHighestPriorityIndex(Operands.Plus, Operands.Minus);
                if(i == -1){
                    break;
                }
                if(operands.get(i) == Operands.Plus){
                    tempResult = Double.parseDouble(numbers.get(i)) + Double.parseDouble(numbers.get(i+1));
                    editNumbersOperands(i, tempResult);
                }
                else if(operands.get(i) == Operands.Minus){
                    tempResult = Double.parseDouble(numbers.get(i)) - Double.parseDouble(numbers.get(i+1));
                    editNumbersOperands(i, tempResult);
                }
            }
        }
    }

    public String getResult(){
        return numbers.get(0);
    }
}
