from graph.node import Node


class GraphCloner:
    def __init__(self):
        self.cloned_nodes = {}

    @staticmethod
    def clone(root_node):
        cloned_nodes = GraphCloner.clone_nodes(root_node)
        return GraphCloner.reconstruct_cloned_node_edges(root_node, cloned_nodes)

    @staticmethod
    def clone_nodes(root_node):
        cloned_nodes = {}

        for node in root_node.iterator():
            cloned_node = Node(node.color, node.node_id)
            cloned_nodes[node] = cloned_node

        return cloned_nodes

    @staticmethod
    def reconstruct_cloned_node_edges(root_node, cloned_nodes):
        for node in root_node.iterator():
            cloned_node = cloned_nodes[node]

            for child_node in node.edges:
                cloned_node.edges.append(cloned_nodes[child_node])

        return cloned_nodes[root_node]
