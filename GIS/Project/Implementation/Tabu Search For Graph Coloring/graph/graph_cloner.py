from graph.node import Node


class GraphCloner:
    def __init__(self):
        self.inspected_nodes = set()
        self.cloned_nodes = {}

    def clone(self, node):
        self.inspected_nodes = set()
        self.fill_in_clone_nodes(node)
        self.inspected_nodes = set()

        return self.reconstruct_cloned_node_edges(node)

    def fill_in_clone_nodes(self, node):
        self.inspected_nodes.add(node)
        cloned_node = Node(node.color)
        self.cloned_nodes[node] = cloned_node

        for child_node in node.edges:
            if child_node not in self.inspected_nodes:
                self.fill_in_clone_nodes(child_node)

    def reconstruct_cloned_node_edges(self, node):
        self.inspected_nodes.add(node)
        cloned_node = self.cloned_nodes[node]

        for child_node in node.edges:
            cloned_node.edges.append(self.cloned_nodes[child_node])

            if child_node not in self.inspected_nodes:
                self.reconstruct_cloned_node_edges(child_node)

        return cloned_node
