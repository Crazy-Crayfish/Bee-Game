
public class ResourceCounter {
    private int nectar = 0;
    private int honey = 0;
    private int wax = 0;
    private int dna = 0;

    //nectar
    public int getNectar() {
        return nectar;
    }
    public void setNectar(int setTo) {
        nectar = setTo;
    }
    public void addNectar(int plusBy) {
        nectar += plusBy;
    }
    public void takeNectar(int minusBy) {
        nectar -= minusBy;
    }

    //honey
    public int getHoney() {
        return honey;
    }
    public void setHoney(int setTo) {
        honey = setTo;
    }
    public void addHoney(int plusBy) {
        honey += plusBy;
    }
    public void takeHoney(int minusBy) {
        honey -= minusBy;
    }

    //wax
    public int getWax() {
        return wax;
    }
    public void setWax(int setTo) {
        wax = setTo;
    }
    public void addWax(int plusBy) {
        wax += plusBy;
    }
    public void takeWax(int minusBy) {
        wax -= minusBy;
    }

    //dna
    public int getDna() {
        return dna;
    }
    public void setDna(int setTo) {
        dna = setTo;
    }
    public void addDna(int plusBy) {
        dna += plusBy;
    }
    public void takeDna(int minusBy) {
        dna -= minusBy;
    }
}
