public class Main {

    public static void main(String[] args) {
        Scanner userInput = new Scanner(System.in);
        String example = userInput.nextLine();
        ParseEngine parser = new ParseEngine();
        parser.parseString(example);
        System.out.println(parser.getResult());
    }
}


class ParseEngine{

    enum Operands{Plus, Minus, Multiply, Division};
    enum Brackets{Open, Close};

    private ArrayList<String> numbers = new ArrayList<>();
    private ArrayList<Operands> operands = new ArrayList<>();

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
    }

    public void parseString(String example){
        resetExample();
        StringBuilder temp = new StringBuilder();

        for(int i = 0; i < example.length(); i++){

            if (!isOperand(example.charAt(i))){
                temp.append(example.charAt(i));
            }
            else if(isOperand(example.charAt(i))){
                numbers.add(String.valueOf(temp));
                temp.setLength(0);
                addOperand(example.charAt(i));
            }

            if(i == example.length() - 1 && temp.length() > 0){
                numbers.add(String.valueOf(temp));
                temp.setLength(0);
            }
        }

        checkCorrectness();
        eval();
    }

    private boolean isHighPriorityOperand(Operands operand){
        return (operand == Operands.Multiply || operand == Operands.Division);
    }

    private boolean isLowPriorityOperand(Operands operand){
        return (operand == Operands.Plus || operand == Operands.Minus);
    }

    private int countOperands(boolean isHighPriority){
        int count = 0;
        for(Operands operand: operands){
            if (isHighPriority){
                if(isHighPriorityOperand(operand)) count++;
            }
            else{
                if(isLowPriorityOperand(operand)) count++;
            }
        }

        return count;
    }

    private void editNumbersOperands(int index, double result){
        numbers.set(index, String.valueOf(result));
        numbers.remove(index + 1);
        operands.remove(index);
    }

    private void checkCorrectness(){
        if (numbers.size() == operands.size()){
            operands.remove(operands.size()-1);
        }
    }

    private void eval(){
        int i = 0;
        double tempResult;
        while(countOperands(true) != 0){
            if(operands.get(i) == Operands.Multiply){
                tempResult = Double.parseDouble(numbers.get(i)) * Double.parseDouble(numbers.get(i+1));
                editNumbersOperands(i, tempResult);
            }
            else if(operands.get(i) == Operands.Division){
                tempResult = Double.parseDouble(numbers.get(i)) / Double.parseDouble(numbers.get(i+1));
                editNumbersOperands(i, tempResult);
            }
            else{
                i++;
            }
        }

        i = 0;
        while(countOperands(false) != 0){
            if(operands.get(i) == Operands.Plus){
                tempResult = Double.parseDouble(numbers.get(i)) + Double.parseDouble(numbers.get(i+1));
                editNumbersOperands(i, tempResult);
            }
            else if(operands.get(i) == Operands.Minus){
                tempResult = Double.parseDouble(numbers.get(i)) - Double.parseDouble(numbers.get(i+1));
                editNumbersOperands(i, tempResult);
            }
            else{
                i++;
            }
        }
    }

    public String getResult(){
        return numbers.get(0);
    }
}
