class Memory:
    def __init__(self, short_term_memory_size):
        self.memory = []
        self.short_term_memory_size = short_term_memory_size

    def add_to_memory(self, node, color):
        self.memory.append((node.node_id, color))

    def clear_memory(self):
        self.memory = []

    def is_in_long_term_memory(self, node, color):
        return (node.node_id, color) in self.memory

    def is_in_short_term_memory(self, node, color):
        return (node.node_id, color) in self.memory[-self.short_term_memory_size:]
