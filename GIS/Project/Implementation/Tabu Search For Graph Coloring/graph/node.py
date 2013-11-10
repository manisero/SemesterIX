class Node:
    Id = 0

    def __init__(self, color=None, node_id=None, previous_color=None):
        self.edges = []
        self.color = color

        if node_id is not None:
            self.node_id = node_id
        else:
            self.node_id = Node.Id
            Node.Id += 1

        self.previous_color = self.color

        if previous_color is not None:
            self.previous_color = previous_color

        self.visited_nodes = []

    def add_edges(self, nodes):
        for node in nodes:
            self.edges.append(node)

    def __getitem__(self, item):
        return self.edges[item]

    def get_node_of_id(self, node_id):
        self.visited_nodes = []
        return self.recursive_search_for_node_of_id(self, node_id)

    def recursive_search_for_node_of_id(self, current_node, node_id):
        self.visited_nodes.append(current_node)

        if current_node.node_id == node_id:
            return current_node

        for child_node in current_node.edges:
            if child_node in self.visited_nodes:
                continue

            found_node = self.recursive_search_for_node_of_id(child_node, node_id)

            if found_node is not None:
                return found_node

        return None
