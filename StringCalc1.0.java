package com.company;
import java.util.ArrayList;
import java.util.Scanner;



public class Main {

    public static void main(String[] args) {
        Scanner userInput = new Scanner(System.in);
        String example = userInput.nextLine();

        ParseEngine parser = new ParseEngine();
        parser.setExample(example);
        System.out.println(parser.getResult());
    }
}


class ParseEngine{
    private String example;

    enum Operation {Plus, Minus, Multiply, Division};
    Operation operation = null;

    private ArrayList<String> evalNumbers = new ArrayList<String>();
    private ArrayList<Operation> evalOperands = new ArrayList<Operation>();

    private StringBuilder temp = new StringBuilder();

    private long resultInt;
    private double resultDouble;

    ParseEngine(){ }

    public void setExample(String example){
        this.example = example;
        evalNumbers.clear();
        evalOperands.clear();
        parseString();
        eval();
    }

    private void checkCorectness(){
        if(evalNumbers.isEmpty()) evalNumbers.add("No value");
        else if(evalNumbers.size() == evalOperands.size()) evalNumbers.add("1");
    }

    private void addOperand(int index){
        if(temp.length() != 0){
            evalNumbers.add(temp.toString());
            switch(example.charAt(index)){
                case '+':
                    evalOperands.add(Operation.Plus);
                    break;
                case '-':
                    evalOperands.add(Operation.Minus);
                    break;
                case '*':
                    evalOperands.add(Operation.Multiply);
                    break;
                case '/':
                    evalOperands.add(Operation.Division);
                    break;
            }
            temp.setLength(0);
        }
    }

    //обрабатывает строку
    private void parseString(){
        for(int i = 0; i < example.length(); i++){
            if(example.charAt(i) == '+' || example.charAt(i) == '-' ||
                    example.charAt(i) == '*' || example.charAt(i) == '/') addOperand(i);
            else temp.append(example.charAt(i));

            if(i == example.length() - 1){
                if(temp.length() != 0){
                    evalNumbers.add(temp.toString());
                    temp.setLength(0);
                }
            }
        }
        checkCorectness();
    }

    private int countOperands(Operation operandFirst, Operation operandSecond){
        int count = 0;
        for(Operation eo: evalOperands){
            if(eo == operandFirst || eo == operandSecond) count++;
        }
        return count;
    }

    private void editEvals(int index, double result){
        evalNumbers.set(index, String.valueOf(result));
        evalNumbers.remove(index+1);
        evalOperands.remove(index);
    }

    private void eval(){
        int i = 0;
        double tempResult;
        while(countOperands(Operation.Multiply, Operation.Division) != 0){
            if(evalOperands.get(i) == Operation.Multiply){
                tempResult = Double.parseDouble(evalNumbers.get(i)) *
                        Double.parseDouble(evalNumbers.get(i+1));
                editEvals(i, tempResult);
            }
            else if(evalOperands.get(i) == Operation.Division){
                tempResult = Double.parseDouble(evalNumbers.get(i)) /
                        Double.parseDouble(evalNumbers.get(i+1));
                editEvals(i, tempResult);
            }
            else{
                i++;
            }
        }

        i = 0;
        while(countOperands(Operation.Plus, Operation.Minus) != 0){
            if(evalOperands.get(i) == Operation.Plus){
                tempResult = Double.parseDouble(evalNumbers.get(i)) +
                        Double.parseDouble(evalNumbers.get(i+1));
                editEvals(i, tempResult);
            }
            else if(evalOperands.get(i) == Operation.Minus){
                tempResult = Double.parseDouble(evalNumbers.get(i)) -
                        Double.parseDouble(evalNumbers.get(i+1));
                editEvals(i, tempResult);
            }
            else{
                i++;
            }
        }
    }

    public String getResult(){
        return evalNumbers.get(0);
    }
}
