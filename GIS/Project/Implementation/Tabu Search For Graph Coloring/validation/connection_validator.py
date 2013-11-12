class ConnectionValidator:
    def __init__(self):
        pass

    @staticmethod
    def is_graph_connected(root_node, amount_of_nodes):
        return root_node.node_count() == amount_of_nodes
