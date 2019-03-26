public interface Command {
    void Do();
    void Undo();
    byte[] Serialize();
}