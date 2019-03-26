using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CommandList {
    Stack<Command> commands;

    public CommandList() {
        this.commands = new Stack<Command>();
    }

    public void Push(Command command) {
        command.Do();
        commands.Push(command);
    }

    public Command Pop() {
        Command command = commands.Pop();
        command.Undo();
        return command;
    }

    public Command Peek() {
        return commands.Peek();
    }

    public int Count() {
        return commands.Count;
    }

    public void Clear() {
        commands.Clear();
    }

    public byte[] Serialize() {
        int size = 1;
        int count = this.Count();
        List<byte[]> serializedCommand = new List<byte[]>();
        foreach (Command command in commands)
        {
            byte[] commandBytes = command.Serialize();
            serializedCommand.Add(commandBytes);
            size += commandBytes.Length + 1;
        }
        serializedCommand.Reverse();

        byte[] result = new byte[size];
        result[0] = (byte)count;
        int lastPos = 1;

        foreach(byte[] bytes in serializedCommand) {
            result[lastPos] = (byte)bytes.Length;
            bytes.CopyTo(result, lastPos + 1);
            lastPos = lastPos + 1 + bytes.Length;
        }

        return result;
    }

    public void DeSerialize(byte[] bytes) {
        this.Clear();

        int count = bytes[0];
        int pos = 1;
        for(int i = 0; i < count; i++) {
            int size = bytes[pos];
            byte[] commandBytes = new byte[size];
            for(int j = 0; j < size; j++) {
                commandBytes[j] = bytes[pos + j + 1];
            }
            string command = Encoding.ASCII.GetString(commandBytes);
            this.Push(this.CreateCommandFromString(command));
            pos += size + 1;
        }
    }

    private Command CreateCommandFromString(string packet) {
        string[] packetArray = packet.Split(':');
        string name = packetArray[0];
        
        switch (name)
        {
            case "SELECTION": 
                GameObject go = GameObject.Find(packetArray[1]);
                GameObject lastTarget = GameObject.Find(packetArray[2]);
                TargetController controller = GameObject.FindWithTag("Tracker").GetComponent<TargetController>();
                return new CommandSelection(controller, go, lastTarget);
            case "ANIMATION":
                go = GameObject.Find(packetArray[1]);
                string triggerOn = packetArray[2];
                string triggerOff = packetArray[3];
                bool shouldStack = packetArray[4] == "1";
                return new CommandAnimation(go, triggerOn, triggerOff, shouldStack);
            default:
                throw new System.Exception("Cannot parse packet");
        }
    }
}