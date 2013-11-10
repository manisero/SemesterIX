from graph.node import Node


class GraphCloner:
    def __init__(self):
        self.cloned_nodes = {}

    def clone(self, root_node):
        self.fill_in_cloned_nodes(root_node)
        return self.reconstruct_cloned_node_edges(root_node)

    def fill_in_cloned_nodes(self, root_node):
        for node in root_node.iterator():
            cloned_node = Node(node.color, node.node_id)
            self.cloned_nodes[node] = cloned_node

    def reconstruct_cloned_node_edges(self, root_node):
        for node in root_node.iterator():
            cloned_node = self.cloned_nodes[node]

            for child_node in node.edges:
                cloned_node.edges.append(self.cloned_nodes[child_node])

        return self.cloned_nodes[root_node]
